using Api.Helpers;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
        public async Task<ActionResult<RecipientSchema>> GetSchema()
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schemas =  await _repository.GetAllAsync(x => x.ClientId == clientId);

            var schema = schemas.FirstOrDefault();
                       
            if (schema != null)
                return Ok(schema);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<RecipientSchema>> PostRecipientSchema(RecipientSchema schema)
        {
            schema.ClientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            RecipientSchemaValidator _validator = new RecipientSchemaValidator();
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
        public async Task<ActionResult<RecipientSchema>> PutRecipientSchema(RecipientSchemaViewModel model)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = await _repository.GetAsync(x => x.ClientId == clientId);

            if (schema == null)
                return NotFound(new ErrorMessageViewModel { ErrorMessage = "Schema Not Found" });

            schema.Schema = JObject.FromObject(model.Schema);
            RecipientSchemaValidator _validator = new RecipientSchemaValidator();
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchema(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
