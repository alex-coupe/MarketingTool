using Api.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MarketingToolTests.BackendTests
{
    public class AuthenticationControllerTests
    {
        Mock<IRepository<User>> _userRepositoryMock;
        Mock<IRepository<PasswordReset>> _passwordResetMock;
        Mock<IConfiguration> _configurationMock;
    
        public AuthenticationControllerTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _passwordResetMock = new Mock<IRepository<PasswordReset>>();
            _configurationMock = new Mock<IConfiguration>();

            var userList = new List<User> {
            new User
            {
                Id = 1,
                EmailAddress = "test@test.com",
                FirstName = "Barry",
                LastName = "Beaverton",
                Admin = true,
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
                Admin = true,
                Archived = false,
                ClientId = 1,
                Password = CryptoHelper.Crypto.HashPassword("Testing123")

            }};
            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(userList);
            _userRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            _userRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(userList);
            _userRepositoryMock.Setup(x => x.ToList()).Returns(userList);
            _configurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns("Iqg3LSKql9HyXsOu1iP4");
        }

        [Fact]
        public void user_is_authenticated_with_correct_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                ,_passwordResetMock.Object);
            LoginRequest request = new LoginRequest();
            request.Email = "test@test.com";
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
            LoginRequest request = new LoginRequest();
            request.Email = "test@test.com";
            request.Password = "Password12";
            var result = _controller.Login(request);

            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorString = Assert.IsType<string>(actionResult.Value);
            var json = errorString;

            Assert.Equal("Email Address or Password Incorrect", json);
        }

        [Fact]
        public async Task well_formed_user_is_registered()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                 , _passwordResetMock.Object);
            var user = new User
            {

                EmailAddress = "barry@test.com",
                FirstName = "Barry",
                LastName = "Beaverton",
                Admin = true,
                Archived = false,
                ClientId = 1,
                Password = "Testing123"
            };

            var result = await _controller.Register(user);
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var response = Assert.IsType<User>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("barry@test.com", response.EmailAddress);
            _userRepositoryMock.Verify(r => r.Add(user));
            _userRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task using_existing_email_does_not_register()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                 , _passwordResetMock.Object);
            var user = new User
            {

                EmailAddress = "garry@test.com",
                FirstName = "Garry",
                LastName = "Beaverton",
                Admin = true,
                Archived = false,
                ClientId = 1,
                Password = "Testing123"
            };

            var result = await _controller.Register(user);
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorString = Assert.IsType<List<ValidationFailure>>(actionResult.Value);
            var json = errorString.FirstOrDefault().ErrorMessage;

            Assert.Equal("An account with that email address already exists", json);
        }

        [Fact]
        public async Task malformed_user_is_not_registered()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object
                 , _passwordResetMock.Object);
            var user = new User
            {
                Admin = true,
                Archived = false,
                ClientId = 1,
            };

            var result = await _controller.Register(user);
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
