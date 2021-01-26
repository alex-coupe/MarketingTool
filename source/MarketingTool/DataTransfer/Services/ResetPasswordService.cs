using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransfer.Interfaces;

namespace DataTransfer.Services
{
    public class ResetPasswordService : IDataService<ResetPasswordViewModel>
    {
        private IHttpService _httpService;
        public ResetPasswordService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<ResetPasswordViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordViewModel> GetSingle()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ResetPasswordViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResetPasswordViewModel> Post(ResetPasswordViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task PostNoReturnContent(ResetPasswordViewModel entity)
        {
             await _httpService.PostNoContentResponse($"api/authentication/updatepassword", entity);
        }

        public Task<ResetPasswordViewModel> Put(ResetPasswordViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
