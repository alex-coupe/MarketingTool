using BackEnd.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
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

            _configurationMock.SetupGet(x => x[It.IsAny<string>()]).Returns("Iqg3LSKql9HyXsOu1iP4");
        }

        [Fact]
        public async Task user_is_authenticated_with_correct_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);

            var result = await _controller.Login("test@test.com", "Password123");
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(actionResult.Value);
            _userRepositoryMock.Verify(r => r.GetAllAsync());
        }

        [Fact]
        public async Task user_is_not_authenticated_with_incorrect_credentials()
        {
            AuthenticationController _controller = new AuthenticationController(_configurationMock.Object, _userRepositoryMock.Object);

            var result = await _controller.Login("test@test.com", "Password12");
            var actionResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Email Address or Password Incorrect", actionResult.Value);
            _userRepositoryMock.Verify(r => r.GetAllAsync());
        }
    }
}
