using BackEnd.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _repository.GetAllAsync();
            return Ok(clients);
        }

        [Authorize]
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
            Validator<Client> _validator = new ClientValidator(_repository);
            var errors = _validator.ValidateModel(client);
            if (errors.Count == 0)
            {
                _repository.Add(client);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostClient", new { id = client.Id }, client);
            }

            return BadRequest(errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Client>> PutClient(Client client)
        {
            Validator<Client> _validator = new ClientValidator(_repository);
            var errors = _validator.ValidateModel(client);
            if (errors.Count == 0)
            {
                _repository.Edit(client);
                await _repository.SaveChangesAsync();

                return Ok(client);
            }

            return BadRequest(errors);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
