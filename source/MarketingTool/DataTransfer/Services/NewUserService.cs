using DataTransfer.Interfaces;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Services
{
    public class NewUserService : IDataService<NewUserViewModel>
    {
        private IHttpService _httpService;
        public NewUserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public Task<NewUserViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NewUserViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<NewUserViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public async Task<NewUserViewModel> Post(NewUserViewModel entity)
        {
            return await _httpService.Post<NewUserViewModel>($"api/users", entity);
        }

        public  Task PostNoReturnContent(NewUserService entity)
        {
            throw new NotImplementedException();
        }

        public Task PostNoReturnContent(NewUserViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<NewUserViewModel> Put(NewUserViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
