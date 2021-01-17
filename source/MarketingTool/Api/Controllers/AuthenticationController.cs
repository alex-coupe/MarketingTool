using Api.Helpers;
using Api.Services;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepository<User> _userRepository;
        private IRepository<PasswordReset> _passwordResetRepository;

        public AuthenticationController(IConfiguration config, IRepository<User> userRepository, IRepository<PasswordReset> passwordRepository )
        {
            _config = config;
            _userRepository = userRepository;
            _passwordResetRepository = passwordRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginRequest credentials)
        {
            var user = AuthenticateUser(credentials.Email, credentials.Password);

            if (user != null)
            {
                var token = GenerateJSONWebToken(user);
                var authResponse = new AuthenticationResponse { TokenType = "Bearer", Token = new JwtSecurityTokenHandler().WriteToken(token), 
                    Expires = token.ValidTo, UserId = user.Id, ClientId = user.ClientId, isAdmin = user.Admin, isArchived = user.Archived, Name = user.FirstName };
                return Ok(JsonSerializer.Serialize(authResponse));
            }

            return Unauthorized(new {ErrorMessage = "Email Address or Password Incorrect" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(User user)
        {
            NewUserValidator _validator = new NewUserValidator(_userRepository);
            var validationResult = _validator.Validate(user);
            if (validationResult.IsValid)
            {
                user.Password = CryptoHelper.Crypto.HashPassword(user.Password);

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();

                return CreatedAtAction("Register", new { id = user.Id }, user);
            }
            return BadRequest(validationResult.Errors);
        }

    

        [AllowAnonymous]
        [Route("ResetPassword/{Token}")]
        [HttpGet]
        public ActionResult<PasswordResetData> GetResetRequest([FromRoute] string Token)
        {
            var request = _passwordResetRepository.GetAll().Where(x => x.Token == Token).FirstOrDefault();

            var resetData = new PasswordResetData
            {
                EmailAddress = request.EmailAddress,
                IsTokenValid = request.DateCreated.AddMinutes(30) > DateTime.Now ? true : false,
                UserId = request.UserId
            };

            return Ok(resetData);
        }

        [Authorize]
        [Route("OpenResetRequests")]
        [HttpGet]
        public async Task<ActionResult<PasswordResetData>> GetResetRequests()
        {
            var requests = await _passwordResetRepository.GetAllAsync();

            return Ok(requests);
        }

        [AllowAnonymous]
        [Route("ResetPassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody] EmailObjectRequest request, [FromServices]PasswordResetService resetService)
        {
            var user = _userRepository.Where(x => x.EmailAddress == request.Email).FirstOrDefault();

            if (user != null)
            {
                await resetService.Push(request.Email, user.Id);
               
                return Ok();
            }
            return NotFound();
        }

        [AllowAnonymous]
        [Route("UpdatePassword")]
        [HttpPost]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            var resetEntry = _passwordResetRepository.Where(x => x.EmailAddress == request.EmailAddress)
                .OrderByDescending(x => x.DateCreated)
                .FirstOrDefault();

            if (resetEntry == null || resetEntry.DateCreated.AddMinutes(30) < DateTime.Now)
            {
                return BadRequest();
            }
            var user = resetEntry.User;

            if (user != null)
            {
                user.Password = CryptoHelper.Crypto.HashPassword(request.Password);
                _passwordResetRepository.Remove(resetEntry.Id);
                await _userRepository.SaveChangesAsync();
                await _passwordResetRepository.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(new { ErrorMessage = "A user could not be found" });
        }

        [Authorize]
        [Route("RemoveResetRequest")]
        [HttpDelete]
        public async Task<ActionResult> RemoveRequest(int id)
        {
            _passwordResetRepository.Remove(id);
            await _passwordResetRepository.SaveChangesAsync();
            return NoContent();
        }

        private JwtSecurityToken GenerateJSONWebToken(User user)
        {
            var secruityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(secruityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Admin", user.Admin.ToString()),
                new Claim("Archived", user.Archived.ToString()),
                new Claim("ClientId", user.ClientId.ToString()),
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.EmailAddress)
            };

            var token = new JwtSecurityToken(null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);

            return token;
        }

        private User AuthenticateUser(string email, string password)
        {
            var user = _userRepository.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
