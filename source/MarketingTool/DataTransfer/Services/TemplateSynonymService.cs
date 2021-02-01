using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class TemplateSynonymService : IDataService<TemplateSynonymViewModel>
    {
        private IHttpService _httpService;
        public TemplateSynonymService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<TemplateSynonymViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TemplateSynonymViewModel>> GetAll()
        {
           return await _httpService.Get<IEnumerable<TemplateSynonymViewModel>>($"api/templatesynonyms");
        }

        public Task<TemplateSynonymViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public Task<TemplateSynonymViewModel> Post(TemplateSynonymViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task PostNoReturnContent(TemplateSynonymViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateSynonymViewModel> Put(TemplateSynonymViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
