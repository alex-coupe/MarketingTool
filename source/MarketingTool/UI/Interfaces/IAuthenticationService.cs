using DataTransfer.DataTransferObjects;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IAuthenticationService
    {
        UserDTO User { get; }

        Task Initialize();

        Task Login(LoginRequest loginRequest);

        Task Logout();
    }
}
