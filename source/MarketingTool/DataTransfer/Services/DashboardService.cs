using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class DashboardService : IDataService<DashboardViewModel>
    {
        private IHttpService _httpService;

        public DashboardService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<DashboardViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DashboardViewModel> GetSingle()
        {
            return await _httpService.Get<DashboardViewModel>($"api/dashboard");
        }

        public Task<IEnumerable<DashboardViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<DashboardViewModel> Post(DashboardViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task PostNoReturnContent(DashboardViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<DashboardViewModel> Put(DashboardViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
