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
    [Authorize(Policy = "RootUsers")]
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
            var client = await _repository.GetAsync(x => x.Id == id);

            if (client != null)
                return Ok(client);

            return NotFound();
        }

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
