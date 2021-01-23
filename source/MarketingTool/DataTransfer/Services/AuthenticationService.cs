using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using DataTransfer.Interfaces;

namespace DataTransfer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
       
        public AuthenticationService(IHttpService httpService, NavigationManager navigationManager, ILocalStorageService localStorageService)
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public UserViewModel User { get; private set; }

         public async Task Initialize()
        {
            User = await _localStorageService.GetItem<UserViewModel>("user");
        }

        public async Task Login(LoginViewModel loginRequest)
        {
            User = await _httpService.Post<UserViewModel>("api/authentication/login", loginRequest);
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("/");
        }

        public async Task Register(RegistrationViewModel registrationViewModel)
        {
           User = await _httpService.Post<UserViewModel>("api/authentication/register", registrationViewModel);
          
            await _localStorageService.SetItem("user", User);
        }
    }
}
