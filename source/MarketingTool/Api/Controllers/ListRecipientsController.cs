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
    public class ListRecipientsController : ControllerBase
    {
        private readonly IRepository<ListRecipient> _repository;
        public ListRecipientsController(IRepository<ListRecipient> repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListRecipient>>> GetListRecipientss()
        {
            IEnumerable<ListRecipient> listRecipients;
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email" && claim.Value == "acoupe@gmail.com"))
            {
                listRecipients = await _repository.GetAllAsync();
            }

            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            listRecipients = await _repository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(listRecipients);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ListRecipient>> GetListRecipient(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var listRecipient = await _repository.GetAsync(x => x.ClientId == clientId, id);

            if (listRecipient != null)
                return Ok(listRecipient);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ListRecipient>> PostList(ListRecipient listRecipient)
        {
            
            ListRecipientValidator _validator = new ListRecipientValidator();
            var validationResult = await _validator.ValidateAsync(listRecipient);
            if (validationResult.IsValid)
            {
                _repository.Add(listRecipient);

                await _repository.SaveChangesAsync();
                return CreatedAtAction("PostListRecipient", new { id = listRecipient.Id }, listRecipient);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ListRecipient>> PutListRecipient(ListRecipient listRecipient)
        {
           
            ListRecipientValidator _validator = new ListRecipientValidator();
            var validationResult = await _validator.ValidateAsync(listRecipient);
            if (validationResult.IsValid)
            {
                _repository.Edit(listRecipient);
                await _repository.SaveChangesAsync();

                return Ok(listRecipient);
            }

            return BadRequest(validationResult.Errors);
        }



        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteListRecipient(int id)
        {
            _repository.Remove(id);

            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
