using DataTransfer.DataTransferObjects;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UI.Interfaces;

namespace UI.Services
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

        public UserDTO User { get; private set; }

         public async Task Initialize()
        {
            User = await _localStorageService.GetItem<UserDTO>("user");
        }

        public async Task Login(LoginRequest loginRequest)
        {
            User = await _httpService.Post<UserDTO>("api/authentication/login", loginRequest);
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("/");
        }
    }
}
