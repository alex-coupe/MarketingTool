using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransfer.Interfaces;

namespace DataTransfer.Services
{
    public class RecipientSchemaService : IDataService<RecipientSchemaViewModel>
    {
        private IHttpService _httpService;

        public RecipientSchemaService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<RecipientSchemaViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipientSchemaViewModel> GetSingle()
        {
            return await _httpService.Get<RecipientSchemaViewModel>($"api/recipientschemas");
        }

        public Task<IEnumerable<RecipientSchemaViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<RecipientSchemaViewModel> Post(RecipientSchemaViewModel entity)
        {
            return await _httpService.Post<RecipientSchemaViewModel>($"api/recipientschemas", entity);
        }

        public Task PostNoReturnContent(RecipientSchemaViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<RecipientSchemaViewModel> Put(RecipientSchemaViewModel entity)
        {
            return await _httpService.Put<RecipientSchemaViewModel>($"api/recipientschemas", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete($"api/recipientschemas/{id}");
        }

    }
}
