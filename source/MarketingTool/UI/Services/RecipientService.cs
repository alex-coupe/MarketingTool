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

        public Task<RecipientViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RecipientViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<RecipientViewModel>>($"api/recipients");
        }

        public Task<RecipientViewModel> Post(RecipientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task PostNoReturnContent(RecipientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<RecipientViewModel> Put(RecipientViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
