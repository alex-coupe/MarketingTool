using BackEnd.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionLevelsController : ControllerBase
    {
        private readonly IRepository<SubscriptionLevel> _repository;

        public SubscriptionLevelsController(IRepository<SubscriptionLevel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionLevel>>> GetSubscriptionLevels()
        {
            var subLevels = await _repository.GetAllAsync();
            return Ok(subLevels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionLevel>> GetSubscriptionLevel(int id)
        {
            var subLevel = await _repository.GetAsync(id);

            if (subLevel != null)
                return Ok(subLevel);

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionLevel>> PostSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            IValidator<SubscriptionLevel> _validator = new SubscriptionLevelValidator();
            if (_validator.Valid(subscriptionLevel))
            {
                _repository.Add(subscriptionLevel);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostSubscriptionLevel", new { id = subscriptionLevel.Id }, subscriptionLevel);
            }

            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<ActionResult<SubscriptionLevel>> PutSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            _repository.Edit(subscriptionLevel);
            await _repository.SaveChangesAsync();

            return Ok(subscriptionLevel);
        }

        [HttpDelete]
        public async Task<ActionResult<SubscriptionLevel>> DeleteSubscriptionLevel(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
