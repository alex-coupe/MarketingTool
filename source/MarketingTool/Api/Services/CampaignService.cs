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
                var _recipientRepository = scope.ServiceProvider.GetService<IRepository<Recipient>>();
                var _listRepository = scope.ServiceProvider.GetService<IRepository<List>>();
                var _synonymRepository = scope.ServiceProvider.GetService<IRepository<TemplateSynonym>>();
                var _listRecipientRepository = scope.ServiceProvider.GetService<IRepository<ListRecipient>>();

                var campaigns = await _campaignsRepository.GetAllAsync(x => !x.ProcessedTimestamp.HasValue 
                && x.SendDate.Date == DateTime.Now.Date
                && x.SendDate.Hour == DateTime.Now.Hour);
                    

                foreach (var campaign in campaigns)
                {
                    if (DateTime.Now.Hour == campaign.SendDate.Hour)
                    {
                        var list = await _listRepository.GetAsync(x => x.Id == campaign.ListId);

                        var template = await _templateRepository.GetAsync(x => x.Id == campaign.TemplateId);

                        var listRecipients = await _listRecipientRepository.GetAllAsync(x => x.ListId == list.Id);
                        SmtpStatusCode code = new SmtpStatusCode();
                        code = SmtpStatusCode.Ok;
                        foreach (var listRecipient in listRecipients)
                        {
                            var recipient = await _recipientRepository.GetAsync(x => x.Id == listRecipient.RecipientId);
                            var content = await TemplateHelper.ConvertSynonymsToValuesAsync(template, _synonymRepository, recipient);
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
