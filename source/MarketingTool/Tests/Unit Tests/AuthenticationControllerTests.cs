using BackEnd.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace MarketingToolTests.Unit_Tests
{
    public class AuthenticationControllerTests
    {
        Mock<IRepository<User>> _userRepositoryMock;
        Mock<IConfiguration> _configurationMock;

        public AuthenticationControllerTests()
        {
            _userRepositoryMock = new Mock<IRepository<User>>();
            _configurationMock = new Mock<IConfiguration>();

            _userRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<User> {
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

            }});
            _userRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _configurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns("Iqg3LSKql9HyXsOu1iP4");
        }

        [Fact]
        public async Task user_is_authenticated_with_correct_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);
            LoginRequest request = new LoginRequest();
            request.Email = "test@test.com";
            request.Password = "Password123";
            var result = await _controller.Login(request);

            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult.Value);
            _userRepositoryMock.Verify(r => r.GetAllAsync());
        }

        [Fact]
        public async Task user_is_not_authenticated_with_incorrect_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);
            LoginRequest request = new LoginRequest();
            request.Email = "test@test.com";
            request.Password = "Password12";
            var result = await _controller.Login(request);

            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var errorString = actionResult.Value.ToString();
            var json = JsonConvert.DeserializeObject<List<Error>>(errorString);

            Assert.Equal("Email Address or Password Incorrect", json.FirstOrDefault().ErrorMessage);
            _userRepositoryMock.Verify(r => r.GetAllAsync());
        }

        [Fact]
        public async Task well_formed_user_is_registered()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);
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
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var response = Assert.IsType<User>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("garry@test.com", response.EmailAddress);
            _userRepositoryMock.Verify(r => r.Add(user));
            _userRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task malformed_user_is_not_registered()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);
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
