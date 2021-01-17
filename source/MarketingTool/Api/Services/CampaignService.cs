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
                    ProcessQueue();
                    await Task.Delay(TimeSpan.FromSeconds(150), token);
                }
            }, token);
        }

        public async void ProcessQueue()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _campaignJobsRepository = scope.ServiceProvider.GetService<IRepository<CampaignJob>>();
                var _campaignJobHistoryRepository = scope.ServiceProvider.GetService<IRepository<CampaignJobHistory>>();
                var _campaignsRepository = scope.ServiceProvider.GetService<IRepository<Campaign>>();
                var _templatesRepository = scope.ServiceProvider.GetService<IRepository<Template>>();
                var _synonymsRepository = scope.ServiceProvider.GetService<IRepository<TemplateSynonym>>();

                var jobs = _campaignJobsRepository.GetAll();

                foreach (var job in jobs)
                {
                    var campaign = _campaignsRepository.Where(x => x.Id == job.CampaignId).FirstOrDefault();
                    var template = _templatesRepository.Where(x => x.Id == campaign.Id).OrderByDescending(x => x.Version).FirstOrDefault();

                    

                    _emailService.Send(new MailAddress(job.RecipientEmail), new MailMessage
                    {
                        IsBodyHtml = true,
                        Subject = campaign.Subject,
                        Body = TemplateHelper.ConvertSynonymsToValues(template, _synonymsRepository)
                    });

                    _campaignJobHistoryRepository.Add(new CampaignJobHistory
                    {
                        CampaignId = job.CampaignId,
                        EmailStatusId = 2,
                        RecipientEmail = job.RecipientEmail,
                        ProcessedTimestamp = DateTime.Now
                    });
                    await _campaignJobHistoryRepository.SaveChangesAsync();

                    campaign.LastSent = DateTime.Now;
                    await _campaignsRepository.SaveChangesAsync();

                    _campaignJobsRepository.Remove(job.Id);
                    await _campaignJobsRepository.SaveChangesAsync();
                }
            }
        }


    }
}
