using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class SubscriptionLevelService : IDataService<SubscriptionLevelViewModel>
    {
        private IHttpService _httpService;

        public SubscriptionLevelService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task<IEnumerable<SubscriptionLevelViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<SubscriptionLevelViewModel>>("api/subscriptionlevels");
        }

        public async Task<SubscriptionLevelViewModel> GetOne(int id)
        {
            return await _httpService.Get<SubscriptionLevelViewModel>($"api/subscriptionlevel/{id}");
        }

        public Task<SubscriptionLevelViewModel> Post(SubscriptionLevelViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<SubscriptionLevelViewModel> Put(SubscriptionLevelViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
