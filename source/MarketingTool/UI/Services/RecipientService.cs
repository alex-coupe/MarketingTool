using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
{
    public class RecipientService : IDataService<RecipientViewModel>
    {
        private IHttpService _httpService;

        public RecipientService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<RecipientViewModel> Get(int id)
        {
            return await _httpService.Get<RecipientViewModel>($"api/recipient/{id}");
        }

        public async Task<IEnumerable<RecipientViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<RecipientViewModel>>($"api/recipients");
        }

        public async Task<RecipientViewModel> Post(RecipientViewModel entity)
        {
            return await _httpService.Post<RecipientViewModel>($"api/recipient", entity);
        }

        public Task PostNoReturnContent(RecipientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipientViewModel> Put(RecipientViewModel entity)
        {
            return await _httpService.Put<RecipientViewModel>($"api/recipient", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete<RecipientViewModel>($"api/recipient/{id}");
        }
    }
}
