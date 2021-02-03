using Api.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace MarketingToolTests.BackendTests
{
    public class AuthenticationControllerTests
    {
        Mock<IRepository<User>> _userRepositoryMock;
        Mock<IRepository<Client>> _clientRepositoryMock;
        Mock<IRepository<PasswordReset>> _passwordResetMock;
        Mock<IConfiguration> _configurationMock;
        List<Client> clientList;
        public AuthenticationControllerTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _passwordResetMock = new Mock<IRepository<PasswordReset>>();
            _configurationMock = new Mock<IConfiguration>();

            _clientRepositoryMock = new Mock<IRepository<Client>>();
            clientList = new List<Client> {
            new Client
            {
                Id = 1,
                Name = "Creative Inc",

                SubscriptionLevelId = 1
            },
             new Client
            {
                Id = 2,
                Name = "Big Bad Barry Inc",

                SubscriptionLevelId = 1

            }};

            var userList = new List<User> {
            new User
            {
                Id = 1,
                EmailAddress = "test@test.com",
                FirstName = "Barry",
                LastName = "Beaverton",
                RoleId = 2,
                Archived = false,
                ClientId = 1,
                Password = CryptoHelper.Crypto.HashPassword("Password123")
        },
             new User
            {
               Id = 1,
                EmailAddress = "garry@test.com",
                FirstName = "Garry",
                LastName = "Beaverton",
                RoleId = 4,
                Archived = false,
                ClientId = 1,
                Password = CryptoHelper.Crypto.HashPassword("Testing123")

            }};
            _userRepositoryMock.Setup(x => x.GetAllAsync(null, null)).ReturnsAsync(userList);
            _userRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _clientRepositoryMock.Setup(x => x.Add(It.IsAny<Client>()));
            
            _configurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns("Iqg3LSKql9HyXsOu1iP4");
        }

        [Fact]
        public void user_is_authenticated_with_correct_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                ,_passwordResetMock.Object);
            LoginViewModel request = new LoginViewModel();
            request.EmailAddress = "test@test.com";
            request.Password = "Password123";
            var result =  _controller.Login(request);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult.Value);
        }

        [Fact]
        public void user_is_not_authenticated_with_incorrect_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                 , _passwordResetMock.Object);
            LoginViewModel request = new LoginViewModel();
            request.EmailAddress = "test@test.com";
            request.Password = "Password12";
            var result = _controller.Login(request);

            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorString = actionResult.Value;
            
            Assert.Equal("{ ErrorMessage = Email Address or Password Incorrect }", errorString.ToString());
        }

    }
}
