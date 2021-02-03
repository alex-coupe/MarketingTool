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
    [Authorize(Policy = "NotArchived")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionLevelsController : ControllerBase
    {
        private readonly IRepository<SubscriptionLevel> _repository;

        public SubscriptionLevelsController(IRepository<SubscriptionLevel> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionLevel>>> GetSubscriptionLevels()
        {
            var subLevels = await _repository.GetAllAsync();
            return Ok(subLevels);
        }

        [Authorize(Policy = "RootUsers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionLevel>> GetSubscriptionLevel(int id)
        {
            var subLevel = await _repository.GetAsync(x => x.Id == id);

            if (subLevel != null)
                return Ok(subLevel);

            return NotFound();
        }

        [Authorize(Policy = "RootUsers")]
        [HttpPost]
        public async Task<ActionResult<SubscriptionLevel>> PostSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            SubscriptionLevelValidator _validator = new SubscriptionLevelValidator(_repository);
            var validationResult = await _validator.ValidateAsync(subscriptionLevel);
            if (validationResult.IsValid)
            {
                _repository.Add(subscriptionLevel);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostSubscriptionLevel", new { id = subscriptionLevel.Id }, subscriptionLevel);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize(Policy = "RootUsers")]
        [HttpPut]
        public async Task<ActionResult<SubscriptionLevel>> PutSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            SubscriptionLevelValidator _validator = new SubscriptionLevelValidator(_repository);
            var validationResult = await _validator.ValidateAsync(subscriptionLevel);
            if (validationResult.IsValid)
            {
                _repository.Edit(subscriptionLevel);
                await _repository.SaveChangesAsync();

                return Ok(subscriptionLevel);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize(Policy = "RootUsers")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubscriptionLevel(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
