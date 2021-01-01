using BackEnd.Controllers;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarketingToolTests.API_Tests
{
    public class SubscriptionLevelsControllerTests
    {
        Mock<IRepository<SubscriptionLevel>> _subscriptionLevelRepositoryMock;
        private int getId = 2;

        public SubscriptionLevelsControllerTests()
        {
            _subscriptionLevelRepositoryMock = new Mock<IRepository<SubscriptionLevel>>();

            _subscriptionLevelRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<SubscriptionLevel> { 
            new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
                Id = 1,
                MaxUsers = 5
            },
            new SubscriptionLevel
            {
                Name = "Pro",
                Cost = 20.99M,
                Id = 2,
                MaxUsers = 20
            }
            });

            _subscriptionLevelRepositoryMock.Setup(x => x.GetAsync(getId)).Returns(Task.FromResult(new SubscriptionLevel
            {
          
                Name = "Pro",
                Cost = 20.99M,
                Id = 2,
                MaxUsers = 20
            
            }));
        }
          
        [Fact]
        public async Task get_returns_all_subscription_levels()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);

            var result = await _controller.GetSubscriptionLevels();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SubscriptionLevel>>(actionResult.Value);
            var level = returnValue.FirstOrDefault();
            Assert.Equal("Free", level.Name);
            Assert.Equal(2, returnValue.Count());

        }

        [Fact]
        public async Task get__by_id_returns_correct_subscription_level()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);

            var result = await _controller.GetSubscriptionLevel(2);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var level = Assert.IsType<SubscriptionLevel>(actionResult.Value);
            Assert.NotNull(level);
            Assert.Equal("Pro", level.Name);
        }
    }
}
