﻿using DataAccess.Interfaces;
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

            return Ok(subLevel);
        }

        [HttpPost]
        public async Task<ActionResult<SubscriptionLevel>> PostSubscriptionLevel(SubscriptionLevel subscriptionLevel)
        {
            if (ModelState.IsValid)
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

            return NoContent();
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
