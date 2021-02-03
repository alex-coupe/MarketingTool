using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class UserService : IDataService<UserViewModel>
    {
        private IHttpService _httpService;
        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<UserViewModel> Get(int id)
        {
            return await _httpService.Get<UserViewModel>($"api/users/{id}");
        }

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            return await _httpService.Get<IEnumerable<UserViewModel>>($"api/users");
        }

        public Task<UserViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewModel> Post(UserViewModel entity)
        {
            return await _httpService.Post<UserViewModel>($"api/users", entity);
        }

        public Task PostNoReturnContent(UserViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewModel> Put(UserViewModel entity)
        {
            return await _httpService.Put<UserViewModel>($"api/users", entity);
        }

        public async Task Remove(int id)
        {
            await _httpService.Delete($"api/users/{id}");
        }
    }
}
