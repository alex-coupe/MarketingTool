using Api.Helpers;
using Api.Services;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Enums;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataMappers;

namespace Api.Controllers
{
    [Authorize(Policy = "NotArchived")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IRepository<User> _userRepository;
        public UsersController(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        [Authorize(Policy = "AdminUsers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var users = await _userRepository.GetAllAsync(x => x.ClientId == clientId, new string[] {"Permissions" });
            users.Map(out List<UserViewModel> usersViewModel);
            return Ok(usersViewModel);
        }

        [Authorize(Policy = "AdminUsers")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var user = await _userRepository.GetAsync(x => x.ClientId == clientId && x.Id == id);

            if (user != null)
            {
                user.Map(out UserViewModel viewModel);
                return Ok(viewModel);
            }

            return NotFound();
        }

        [Authorize(Policy = "AdminUsers")]
        [Route("Invite")]
        [HttpPost]
        public async Task<ActionResult> PostUserInvite([FromBody] EmailAddressViewModel request, [FromServices] UserInviteService userInviteService)
        {
            var userId = AuthHelper.GetUserId(HttpContext.User.Claims);
            await userInviteService.Push(request.EmailAddress, userId);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.RoleId = (int)RolesEnum.User;
            UserValidator _validator = new UserValidator(_userRepository);
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
            var userId = AuthHelper.GetUserId(HttpContext.User.Claims);
            var isAdmin = AuthHelper.CheckIfAdmin(HttpContext.User.Claims);

            if (user.Id == userId || isAdmin)
            {
                UserValidator _validator = new UserValidator(_userRepository);
                var validationResult = await _validator.ValidateAsync(user);
                if (validationResult.IsValid)
                {
                    _userRepository.Edit(user);
                    await _userRepository.SaveChangesAsync();

                    return Ok(user);
                }
                return BadRequest(validationResult.Errors);
            }

            return Unauthorized();
        }

        [Authorize(Policy = "AdminUsers")]
        [Route("Archive")]
        [Authorize]
        [HttpPut]
        public async Task<ActionResult<User>> ArchiveUser(User user)
        {
            var userId = AuthHelper.GetUserId(HttpContext.User.Claims);

            if (user.Id != userId)
            {
                user.Archived = true;
                UserValidator _validator = new UserValidator(_userRepository);
                var validationResult = await _validator.ValidateAsync(user);
                if (validationResult.IsValid)
                {
                    _userRepository.Edit(user);
                    await _userRepository.SaveChangesAsync();

                    return Ok(user);
                }
            }
            return BadRequest(new ErrorMessageViewModel {ErrorMessage = "Cannot Archive Yourself" });
 
        }

        [Authorize(Policy = "AdminUsers")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            _userRepository.Remove(id);

            await _userRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
