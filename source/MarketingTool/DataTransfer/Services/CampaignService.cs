using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class CampaignService : IDataService<CampaignViewModel>
    {
        private IHttpService _httpService;
        public CampaignService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<CampaignViewModel> Get(int id)
        {
            return await _httpService.Get<CampaignViewModel>($"api/campaigns/{id}");
        }

        public Task<CampaignViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CampaignViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<CampaignViewModel>>($"api/campaigns");
        }

        public async Task<CampaignViewModel> Post(CampaignViewModel entity)
        {
            return await _httpService.Post<CampaignViewModel>($"api/campaigns", entity);
        }

        public Task PostNoReturnContent(CampaignViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CampaignViewModel> Put(CampaignViewModel entity)
        {
            return await _httpService.Put<CampaignViewModel>($"api/campaigns", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete($"api/campaigns/{id}");
        }

       
    }
}
