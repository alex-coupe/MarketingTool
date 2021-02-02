using Api.Helpers;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateSynonymsController : ControllerBase
    {
        private IRepository<TemplateSynonym> _repository;
        private IRepository<RecipientSchema> _schemaRepository;
        public TemplateSynonymsController(IRepository<TemplateSynonym> repository, IRepository<RecipientSchema> schemaRepository)
        {
            _repository = repository;
            _schemaRepository = schemaRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateSynonym>>> GetTemplateSynonyms()
        {
            
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var synonyms = await _repository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(synonyms);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateSynonym>> GetTemplateSynonym(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var synonym = await _repository.GetAsync(x => x.ClientId == clientId && x.Id == id);

            if (synonym != null)
                return Ok(synonym);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TemplateSynonym>> PostTemplateSynonym(TemplateSynonym synonym)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = _schemaRepository.Where(x => x.ClientId == clientId).FirstOrDefault();
            TemplateSynonymValidator _validator = new TemplateSynonymValidator(schema);
            var validationResult = await _validator.ValidateAsync(synonym);
            if (validationResult.IsValid)
            {
                _repository.Add(synonym);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostTemplateSynonym", new { id = synonym.Id }, synonym);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<TemplateSynonym>> PutTemplateSynonym(TemplateSynonym synonym)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = _schemaRepository.Where(x => x.ClientId == clientId).FirstOrDefault();
            TemplateSynonymValidator _validator = new TemplateSynonymValidator(schema);
            var validationResult = await _validator.ValidateAsync(synonym);
            if (validationResult.IsValid)
            {
                _repository.Edit(synonym);
                await _repository.SaveChangesAsync();

                return Ok(synonym);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTemplateSynonym(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
