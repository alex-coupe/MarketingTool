using Api.Helpers;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientsController : ControllerBase
    {
        private IRepository<Recipient> _repository;
        private IRepository<RecipientSchema> _schemaRepository;
        public RecipientsController(IRepository<Recipient> repository, IRepository<RecipientSchema> schemaRepository)
        {
            _repository = repository;
            _schemaRepository = schemaRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipient>>> GetRecipients()
        {
            IEnumerable<Recipient> recipients;
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email" && claim.Value == "acoupe@gmail.com"))
            {
                recipients = await _repository.GetAllAsync();
            }

            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            recipients = await _repository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(recipients);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipient>> GetSchema(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = await _repository.GetAsync(x => x.ClientId == clientId, id);

            if (schema != null)
                return Ok(schema);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Recipient>> PostRecipient(Recipient recipient)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema =  _schemaRepository.Where(x => x.ClientId == clientId).FirstOrDefault();
            RecipientValidator _validator = new RecipientValidator(schema);
            var validationResult = await _validator.ValidateAsync(recipient);
            if (validationResult.IsValid)
            {
                _repository.Add(recipient);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostRecipient", new { id = recipient.Id }, recipient);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [Route("ImportRecipients")]
        [HttpPost]
        public async Task<ActionResult> PostRecipientsAsync(IFormFile file, [FromServices]IConfiguration configuration)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = _schemaRepository.Where(x => x.ClientId == clientId).FirstOrDefault();
            int rejectedImports = 0;
            int acceptedImports = 0;
            
            var recipientsList = await UploadHelpers.ImportUpload(file, schema, configuration);

            if (recipientsList == null)
                return BadRequest();

            foreach(var recipient in recipientsList)
            {
                RecipientValidator _validator = new RecipientValidator(schema);
                var validationResult = await _validator.ValidateAsync(recipient);
                if (validationResult.IsValid)
                {
                    _repository.Add(recipient);
                    acceptedImports++;
                }
                else
                {
                    rejectedImports++;
                }
                
            }

            await _repository.SaveChangesAsync();

            return Ok(new { RejectedImports = rejectedImports, AcceptedImports = acceptedImports });
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Recipient>> PutRecipient(Recipient recipient)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var schema = _schemaRepository.Where(x => x.ClientId == clientId).FirstOrDefault();
            RecipientValidator _validator = new RecipientValidator(schema);
            var validationResult = await _validator.ValidateAsync(recipient);
            if (validationResult.IsValid)
            {
                _repository.Edit(recipient);
                await _repository.SaveChangesAsync();

                return Ok(recipient);
            }

            return BadRequest(validationResult.Errors);
        }



        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteRecipient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }


    }
}
