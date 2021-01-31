using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class TemplateService : IDataService<TemplateViewModel>
    {
        private IHttpService _httpService;
        public TemplateService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<TemplateViewModel> Get(int id)
        {
            return await _httpService.Get<TemplateViewModel>($"api/templates/{id}");
        }

        public Task<TemplateViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TemplateViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<TemplateViewModel>>($"api/templates");
        }

        public async Task<TemplateViewModel> Post(TemplateViewModel entity)
        {
            return await _httpService.Post<TemplateViewModel>($"api/templates", entity);
        }

        public Task PostNoReturnContent(TemplateViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateViewModel> Put(TemplateViewModel entity)
        {
            return await _httpService.Put<TemplateViewModel>($"api/templates", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete($"api/templates/{id}");
        }

    }
}
