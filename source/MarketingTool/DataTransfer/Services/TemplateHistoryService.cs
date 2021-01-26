using DataAccess.Models;
using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class TemplateHistoryService : IDataService<TemplateHistoryViewModel>
    {
        private IHttpService _httpService;
        public TemplateHistoryService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<TemplateHistoryViewModel> Get(int id)
        {
            return await _httpService.Get<TemplateHistoryViewModel>($"api/templatehistory/?templateId={id}");
        }

        public Task<TemplateHistoryViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemplateHistoryViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TemplateHistoryViewModel> Post(TemplateHistoryViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task PostNoReturnContent(TemplateHistoryViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<TemplateHistoryViewModel> Put(TemplateHistoryViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
