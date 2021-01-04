using BackEnd.Controllers;
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
    public class ClientsControllerTests
    {
        Mock<IRepository<Client>> _clientRepositoryMock;
        private int _getId = 1;

        public ClientsControllerTests()
        {
            _clientRepositoryMock = new Mock<IRepository<Client>>();

            _clientRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(new List<Client> {
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

            }});

            _clientRepositoryMock.Setup(x => x.GetAsync(_getId)).Returns(Task.FromResult(new Client
            {
                Id = 1,
                Name = "Creative Inc",


                SubscriptionLevelId = 1
            }));

            _clientRepositoryMock.Setup(r => r.Edit(It.IsAny<Client>()));
            _clientRepositoryMock.Setup(r => r.Remove(It.IsAny<int>()));
        }

        [Fact]
        public async Task get_returns_all_clients()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);

            var result = await _controller.GetClients();

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<Client>>(actionResult.Value);
            var client = returnValue.FirstOrDefault();
            Assert.Equal("Creative Inc", client.Name);
            Assert.Equal(2, returnValue.Count());
            _clientRepositoryMock.Verify(r => r.GetAllAsync());
        }

        [Fact]
        public async Task get_by_id_returns_correct_client()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);

            var result = await _controller.GetClient(_getId);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var client = Assert.IsType<Client>(actionResult.Value);
            Assert.NotNull(client);
            Assert.Equal("Creative Inc", client.Name);
            _clientRepositoryMock.Verify(r => r.GetAsync(client.Id));
        }

        [Fact]
        public async Task get_by_id_returns_not_found()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);

            var result = await _controller.GetClient(4);

            var actionResult = Assert.IsType<NotFoundResult>(result.Result);

            _clientRepositoryMock.Verify(r => r.GetAsync(4));
        }

        [Fact]
        public async Task post_adds_new_client()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);
            var client = new Client
            {
                Name = "Creative Inc",

                SubscriptionLevelId = 1
            };

            var result = await _controller.PostClient(client);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<Client>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Creative Inc", response.Name);
            _clientRepositoryMock.Verify(r => r.Add(client));
        }

        [Fact]
        public async Task post_invalid_model_returns_bad_request()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);
            var client = new Client
            {
                SubscriptionLevelId = 1
            };

            var result = await _controller.PostClient(client);

            Assert.IsType<BadRequestObjectResult>(result.Result);

        }

        [Fact]
        public async Task edit_alters_client()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);
            var client = new Client
            {
                Name = "Creative Inc",
                SubscriptionLevelId = 1
            };

            await _controller.PostClient(client);

            client.Name = "Creative Industries";

            var result = await _controller.PutClient(client);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Client>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Creative Industries", response.Name);
            _clientRepositoryMock.Verify(r => r.Edit(client));

        }

        [Fact]
        public async Task delete_removes_client()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);
            var client = new Client
            {
                Name = "Creative Inc",
                SubscriptionLevelId = 1
            };

            await _controller.PostClient(client);

            var result = await _controller.DeleteClient(client.Id);

            var actionResult = Assert.IsType<NoContentResult>(result.Result);
            _clientRepositoryMock.Verify(r => r.Remove(client.Id));
        }
    }
}
