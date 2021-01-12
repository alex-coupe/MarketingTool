﻿using Api.Controllers;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace MarketingToolTests.BackendTests
{
    public class ClientsControllerTests
    {
        Mock<IRepository<Client>> _clientRepositoryMock;
        private int _getId = 1;
        List<Client> clientList;

        public ClientsControllerTests()
        {
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

            _clientRepositoryMock.Setup(x => x.GetAsync(_getId)).Returns(Task.FromResult(new Client
            {
                Id = 1,
                Name = "Creative Inc",


                SubscriptionLevelId = 1
            }));
            _clientRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(clientList);

            _clientRepositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
            _clientRepositoryMock.Setup(r => r.Edit(It.IsAny<Client>()));
            _clientRepositoryMock.Setup(r => r.Remove(It.IsAny<int>()));
            _clientRepositoryMock.Setup(x => x.Where(It.IsAny<Expression<Func<Client, bool>>>()))
              .Returns(clientList);
            _clientRepositoryMock.Setup(x => x.ToList()).Returns(clientList);
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
                Name = "Not Very Creative Inc",

                SubscriptionLevelId = 1
            };

            var result = await _controller.PostClient(client);

            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var response = Assert.IsType<Client>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Not Very Creative Inc", response.Name);
            _clientRepositoryMock.Verify(r => r.Add(client));
            _clientRepositoryMock.Verify(r => r.SaveChangesAsync());
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
            var client = clientList.Where(x => x.Id == 1).FirstOrDefault();
            client.Name = "Creative Industries";

            var result = await _controller.PutClient(client);

            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<Client>(actionResult.Value);
            Assert.NotNull(response);
            Assert.Equal("Creative Industries", response.Name);
            _clientRepositoryMock.Verify(r => r.Edit(client));
            _clientRepositoryMock.Verify(r => r.SaveChangesAsync());
        }

        [Fact]
        public async Task malformed_edit_fails()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);

            var client = clientList.Where(x => x.Id == 1).FirstOrDefault();
           
            client.Name = "";

            var result = await _controller.PutClient(client);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task delete_removes_client()
        {
            ClientsController _controller = new ClientsController(_clientRepositoryMock.Object);
            var client = new Client
            {
                Id = 1, 
                Name = "Creative Inc",
                SubscriptionLevelId = 1
            };

            await _controller.PostClient(client);

            var result = await _controller.DeleteClient(client.Id);

            Assert.IsType<NoContentResult>(result);
            _clientRepositoryMock.Verify(r => r.Remove(client.Id));
        }
    }
}
