using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class CampaignJobBuilderService : BackgroundService
    {

        IServiceScopeFactory _serviceScopeFactory;

        public CampaignJobBuilderService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    BuildQueue();
                    await Task.Delay(TimeSpan.FromSeconds(150), token);
                }
            }, token);
        }


        public async void BuildQueue()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _campaignJobsRepository = scope.ServiceProvider.GetService<IRepository<CampaignJob>>();
                var _templateRepository = scope.ServiceProvider.GetService<IRepository<Template>>();
                var _campaignsRepository = scope.ServiceProvider.GetService<IRepository<Campaign>>();
                var _timestepRepository = scope.ServiceProvider.GetService<IRepository<Timestep>>();
                var _listRepository = scope.ServiceProvider.GetService<IRepository<List>>();
                var _synonymRepository = scope.ServiceProvider.GetService<IRepository<TemplateSynonym>>();

                var campaigns = _campaignsRepository.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).ToList();

                foreach (var campaign in campaigns)
                {
                    var timestep = _timestepRepository.Where(x => x.Id == campaign.TimestepId).FirstOrDefault();

                    var list = await _listRepository.GetAsync(campaign.ListId);

                    int hourToSend = 0;

                    if (!campaign.LastSent.HasValue)
                    {
                        hourToSend += timestep.Hours;
                    }
                    else
                    {
                        hourToSend = timestep.Hours + campaign.LastSent.Value.Hour;
                    }

                    var template = await _templateRepository.GetAsync(campaign.TemplateId);

                   

                    foreach (var recipient in list.Recipients)
                    {
                        var content = TemplateHelper.ConvertSynonymsToValues(template, _synonymRepository, recipient);
                        _campaignJobsRepository.Add(new CampaignJob
                        {
                            CampaignId = campaign.Id,
                            HourToProcess = hourToSend,
                            RecipientEmail = recipient.EmailAddress,
                            Content = content,
                            Subject = campaign.Subject,
                            SenderEmail = campaign.SenderEmail
                        });
                    }
                }

                await _campaignJobsRepository.SaveChangesAsync();
            }
        }
    }
}
