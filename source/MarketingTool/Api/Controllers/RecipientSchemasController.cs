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
    public class RecipientSchemasController : ControllerBase
    {
        private IRepository<RecipientSchema> _repository;
        public RecipientSchemasController(IRepository<RecipientSchema> repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipientSchema>>> GetSchemas()
        {
            IEnumerable<RecipientSchema> schemas;
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email" && claim.Value == "acoupe@gmail.com"))
            {
                schemas = await _repository.GetAllAsync();
            }

            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            schemas = await _repository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(schemas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipientSchema>> GetSchema(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = await _repository.GetAsync(x => x.ClientId == clientId, id);
                       
            if (schema != null)
                return Ok(schema);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RecipientSchema>> PostRecipientSchema(RecipientSchema schema)
        {
            PostRecipientSchemaValidator _validator = new PostRecipientSchemaValidator();
            var validationResult = await _validator.ValidateAsync(schema);
            if (validationResult.IsValid)
            {
                _repository.Add(schema);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostRecipientSchema", new { id = schema.Id }, schema);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<RecipientSchema>> PutRecipientSchema(RecipientSchema schema)
        {
            EditRecipientSchemaValidator _validator = new EditRecipientSchemaValidator();
            var validationResult = await _validator.ValidateAsync(schema);
            if (validationResult.IsValid)
            {
                _repository.Edit(schema);
                await _repository.SaveChangesAsync();

                return Ok(schema);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteSchema(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
