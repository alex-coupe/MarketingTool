using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class UserInviteService : IDataService<UserInviteViewModel>
    {
        private IHttpService _httpService;
        public UserInviteService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<UserInviteViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserInviteViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserInviteViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public  Task<UserInviteViewModel> Post(UserInviteViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task PostNoReturnContent(UserInviteViewModel entity)
        {
             await _httpService.Post<UserInviteViewModel>($"api/users/invite", entity);
        }

        public Task<UserInviteViewModel> Put(UserInviteViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
