using Api.Helpers;
using Api.Services;
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
    public class UsersController : ControllerBase
    {
        private IRepository<User> _userRepository;
        public UsersController(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            IEnumerable<User> users;
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email" && claim.Value == "acoupe@gmail.com"))
            {
                users = await _userRepository.GetAllAsync();
            }

            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            users = await _userRepository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(users);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var user = await _userRepository.GetAsync(x => x.ClientId == clientId, id);

            if (user != null)
                return Ok(user);

            return NotFound();
        }

        [Authorize]
        [Route("Invite")]
        [HttpPost]
        public async Task<ActionResult> PostUserInvite([FromBody] string emailAddress, [FromServices] UserInviteService userInviteService)
        {
            var userId = AuthHelper.GetUserId(HttpContext.User.Claims);
            await userInviteService.Push(emailAddress, userId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Template>> PostUser(User user)
        {
            NewUserValidator _validator = new NewUserValidator(_userRepository);
            var validationResult = await _validator.ValidateAsync(user);
            if (validationResult.IsValid)
            {
                _userRepository.Add(user);

                await _userRepository.SaveChangesAsync();
                return CreatedAtAction("PostUser", new { id = user.Id }, user);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<User>> PutUser(User user)
        {
            EditUserValidator _validator = new EditUserValidator(_userRepository);
            var validationResult = await _validator.ValidateAsync(user);
            if (validationResult.IsValid)
            {
                _userRepository.Edit(user);
                await _userRepository.SaveChangesAsync();

                return Ok(user);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            _userRepository.Remove(id);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
