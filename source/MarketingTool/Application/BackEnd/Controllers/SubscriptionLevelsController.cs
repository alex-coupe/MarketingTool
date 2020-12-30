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
    }
}
