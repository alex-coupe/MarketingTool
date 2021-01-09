﻿using BackEnd.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MarketingToolTests.Unit_Tests
{
    public class SubscriptionLevelsControllerTests
    {
        Mock<IRepository<SubscriptionLevel>> _subscriptionLevelRepositoryMock;
        private int _getId = 2;

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

            _subscriptionLevelRepositoryMock.Setup(x => x.GetAsync(_getId)).Returns(Task.FromResult(new SubscriptionLevel
            {

                Name = "Pro",
                Cost = 20.99M,
                Id = 2,
                MaxUsers = 20

            }));
            _subscriptionLevelRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _subscriptionLevelRepositoryMock.Setup(r => r.Edit(It.IsAny<SubscriptionLevel>()));
            _subscriptionLevelRepositoryMock.Setup(r => r.Remove(It.IsAny<int>()));
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
            _subscriptionLevelRepositoryMock.Verify(r => r.GetAllAsync());
        }

        [Fact]
        public async Task get_by_id_returns_correct_subscription_level()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);

            var result = await _controller.GetSubscriptionLevel(_getId);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var level = Assert.IsType<SubscriptionLevel>(actionResult.Value);
            Assert.NotNull(level);
            Assert.Equal("Pro", level.Name);
            _subscriptionLevelRepositoryMock.Verify(r => r.GetAsync(level.Id));
        }

        [Fact]
        public async Task get_by_id_returns_not_found()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);

            var result = await _controller.GetSubscriptionLevel(4);

            var actionResult = Assert.IsType<NotFoundResult>(result.Result);

            _subscriptionLevelRepositoryMock.Verify(r => r.GetAsync(4));
        }

        [Fact]
        public async Task post_adds_new_subscription_level()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);
            var level = new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
                MaxUsers = 5
            };

            var result = await _controller.PostSubscriptionLevel(level);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<SubscriptionLevel>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Free", response.Name);
            _subscriptionLevelRepositoryMock.Verify(r => r.Add(level));
            _subscriptionLevelRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task post_invalid_model_returns_bad_request()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);
            var level = new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
            };

            var result = await _controller.PostSubscriptionLevel(level);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public async Task edit_alters_subscription_level()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);
            var level = new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
                MaxUsers = 5
            };

            await _controller.PostSubscriptionLevel(level);

            level.Name = "Not Free";

            var result = await _controller.PutSubscriptionLevel(level);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<SubscriptionLevel>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Not Free", response.Name);
            _subscriptionLevelRepositoryMock.Verify(r => r.Edit(level));
            _subscriptionLevelRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task malformed_edit_fails()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);
            var level = new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
                MaxUsers = 5
            };

            await _controller.PostSubscriptionLevel(level);

            level.Name = "";

            var result = await _controller.PutSubscriptionLevel(level);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task delete_removes_subscription_level()
        {
            SubscriptionLevelsController _controller = new SubscriptionLevelsController(_subscriptionLevelRepositoryMock.Object);
            var level = new SubscriptionLevel
            {
                Name = "Free",
                Cost = 0.00M,
                MaxUsers = 5
            };

            await _controller.PostSubscriptionLevel(level);



            var result = await _controller.DeleteSubscriptionLevel(level.Id);

            var actionResult = Assert.IsType<NoContentResult>(result.Result);
            _subscriptionLevelRepositoryMock.Verify(r => r.Remove(level.Id));
        }
    }
}