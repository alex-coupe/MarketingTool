using BackEnd.Validators;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IRepository<Client> _repository;

        public ClientsController(IRepository<Client> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _repository.GetAllAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _repository.GetAsync(id);

            if (client != null)
                return Ok(client);

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            IValidator<Client> _validator = new ClientValidator();
            if (_validator.Valid(client))
            {
                _repository.Add(client);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostClient", new { id = client.Id }, client);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult<Client>> PutClient(Client client)
        {
            _repository.Edit(client);
            await _repository.SaveChangesAsync();

            return Ok(client);
        }

        [HttpDelete]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
