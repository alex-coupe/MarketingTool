using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CampaignService : BackgroundService
    {
        IServiceScopeFactory _serviceScopeFactory;
        EmailService _emailService;
        bool mutex = false;
        public CampaignService(IServiceScopeFactory serviceScopeFactory, EmailService emailService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _emailService = emailService;
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (!mutex)
                    {
                        ProcessQueue();
                        await Task.Delay(TimeSpan.FromSeconds(150), token);
                    }
                }
            }, token);
        }

        public async void ProcessQueue()
        {
            mutex = true;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
               
                var _campaignJobHistoryRepository = scope.ServiceProvider.GetService<IRepository<CampaignJobHistory>>();
                var _templateRepository = scope.ServiceProvider.GetService<IRepository<Template>>();
                var _campaignsRepository = scope.ServiceProvider.GetService<IRepository<Campaign>>();
                var _timestepRepository = scope.ServiceProvider.GetService<IRepository<Timestep>>();
                var _recipientRepository = scope.ServiceProvider.GetService<IRepository<Recipient>>();
                var _listRepository = scope.ServiceProvider.GetService<IRepository<List>>();
                var _synonymRepository = scope.ServiceProvider.GetService<IRepository<TemplateSynonym>>();
                var _listRecipientRepository = scope.ServiceProvider.GetService<IRepository<ListRecipient>>();

                var campaigns = _campaignsRepository.Where(x => !x.ProcessedTimestamp.HasValue && x.SendDate.Date == DateTime.Now.Date && x.SendDate.Hour == DateTime.Now.Hour)
                    .ToList();

                foreach (var campaign in campaigns)
                {
                    if (DateTime.Now.Hour == campaign.SendDate.Hour)
                    {
                        var list = await _listRepository.GetAsync(campaign.ListId);

                        var template = _templateRepository.Where(x => x.Id == campaign.TemplateId).FirstOrDefault();

                        var listRecipients = _listRecipientRepository.Where(x => x.ListId == list.Id).ToList();
                        SmtpStatusCode code = new SmtpStatusCode();
                        code = SmtpStatusCode.Ok;
                        foreach (var listRecipient in listRecipients)
                        {
                            var recipient = _recipientRepository.Where(x => x.Id == listRecipient.RecipientId).FirstOrDefault();
                            var content = TemplateHelper.ConvertSynonymsToValues(template, _synonymRepository, recipient);
                            try
                            {
                                _emailService.Send(new MailAddress(recipient.EmailAddress), new MailMessage
                                {
                                    IsBodyHtml = true,
                                    Subject = campaign.Subject,
                                    Body = content
                                });
                            }
                            catch (SmtpFailedRecipientsException ex)
                            {
                                code = ex.StatusCode;
                            }
                            
                            _campaignJobHistoryRepository.Add(new CampaignJobHistory
                            {
                                CampaignId = campaign.Id,
                                EmailStatusCode = (int)code,
                                RecipientEmail = recipient.EmailAddress,
                                ProcessedTimestamp = DateTime.Now
                            });
                            await _campaignJobHistoryRepository.SaveChangesAsync();
                            //For mailtrap
                            Thread.Sleep(10000);
                        }
                        campaign.ProcessedTimestamp = DateTime.Now;
                        campaign.IsActive = false;
                        await _campaignsRepository.SaveChangesAsync();
                        
                    }
                }
            }
            mutex = false;
        }


    }
}
