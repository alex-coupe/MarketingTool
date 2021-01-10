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
        private readonly IRepository<User> _repository;

        public AuthenticationController(IConfiguration config, IRepository<User> repository)
        {
            _config = config;
            _repository = repository;
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

            var errorResponse = new Error { ErrorMessage = "Email Address or Password Incorrect" };
            return Unauthorized(errorResponse);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(User user)
        {
            NewUserValidator _validator = new NewUserValidator(_repository);
            var validationResult = _validator.Validate(user);
            if (validationResult.IsValid)
            {
                user.Password = CryptoHelper.Crypto.HashPassword(user.Password);

                _repository.Add(user);
                await _repository.SaveChangesAsync();

                return CreatedAtAction("Register", new { id = user.Id }, user);
            }
            return BadRequest(validationResult.Errors);
        }

        private JwtSecurityToken GenerateJSONWebToken(User user)
        {
            var secruityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(secruityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Admin", user.Admin.ToString()),
                new Claim("Archived", user.Archived.ToString())
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
            var user = _repository.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
