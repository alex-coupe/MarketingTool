using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IAuthenticationService
    {
        UserViewModel User { get; }

        Task Initialize();

        Task Login(LoginViewModel loginRequest);

        Task Register(RegistrationViewModel registrationViewModel);

        Task Logout();
    }
}
