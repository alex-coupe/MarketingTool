using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
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

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Client>> PutClient(Client client)
        {
            ClientValidator _validator = new ClientValidator();
            var validationResult = await _validator.ValidateAsync(client);
            if (validationResult.IsValid)
            {
                _repository.Edit(client);
                await _repository.SaveChangesAsync();

                return Ok(client);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
