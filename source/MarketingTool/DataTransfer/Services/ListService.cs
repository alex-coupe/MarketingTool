using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class ListService : IDataService<ListViewModel>
    {
        private IHttpService _httpService;

        public ListService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<ListViewModel> Get(string id)
        {
            return await _httpService.Get<ListViewModel>($"api/lists/{id}");
        }

        public Task<ListViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ListViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<ListViewModel>>($"api/lists");
        }

        public async Task<ListViewModel> Post(ListViewModel entity)
        {
            return await _httpService.Post<ListViewModel>($"api/lists", entity);
        }

        public Task PostNoReturnContent(ListViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ListViewModel> Put(ListViewModel entity)
        {
            return await _httpService.Put<ListViewModel>($"api/lists", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete<ListViewModel>($"api/lists/{id}");
        }


    }
}
