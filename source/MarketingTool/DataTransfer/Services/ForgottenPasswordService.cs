using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransfer.Interfaces;

namespace DataTransfer.Services
{
    public class ForgottenPasswordService : IDataService<EmailAddressViewModel>
    {
        private IHttpService _httpService;

        public ForgottenPasswordService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public Task<EmailAddressViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EmailAddressViewModel> Get()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmailAddressViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<EmailAddressViewModel> Post(EmailAddressViewModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task PostNoReturnContent(EmailAddressViewModel entity)
        {
            await _httpService.PostNoContentResponse($"api/authentication/forgottenpassword", entity);
        }

        public Task<EmailAddressViewModel> Put(EmailAddressViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
