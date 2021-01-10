﻿using BackEnd.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubscriptionLevel>>> GetSubscriptionLevels()
        {
            var subLevels = await _repository.GetAllAsync();
            return Ok(subLevels);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubscriptionLevel>> GetSubscriptionLevel(int id)
        {
            var subLevel = await _repository.GetAsync(id);

            if (subLevel != null)
                return Ok(subLevel);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<SubscriptionLevel>> PostSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            Validator<SubscriptionLevel> _validator = new SubscriptionLevelValidator(_repository);
            var errors = _validator.ValidateModel(subscriptionLevel, Type.Post);
            if (!errors.Any())
            {
                _repository.Add(subscriptionLevel);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostSubscriptionLevel", new { id = subscriptionLevel.Id }, subscriptionLevel);
            }

            return BadRequest(errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<SubscriptionLevel>> PutSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            Validator<SubscriptionLevel> _validator = new SubscriptionLevelValidator(_repository);
            var errors = _validator.ValidateModel(subscriptionLevel, Type.Put);
            if (!errors.Any())
            {
                _repository.Edit(subscriptionLevel);
                await _repository.SaveChangesAsync();

                return Ok(subscriptionLevel);
            }

            return BadRequest(errors);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<SubscriptionLevel>> DeleteSubscriptionLevel(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
