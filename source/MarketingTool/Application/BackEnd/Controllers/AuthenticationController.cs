using BackEnd.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
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
using System.Threading.Tasks;

namespace BackEnd.Controllers
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
        [HttpGet]
        [Route("Login")]
        public async Task<ActionResult> Login(string email, string password)
        {
            var user = await AuthenticateUser(email, password);

            if (user != null)
            {
                var token = GenerateJSONWebToken(user);
                return Ok(new { token_type = "Bearer", token = new JwtSecurityTokenHandler().WriteToken(token), expires = token.ValidTo });
            }
            return Unauthorized("Email Address or Password Incorrect");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(User user)
        {
            IValidator<User> _validator = new UserValidator();
            if (_validator.Valid(user))
            {
                user.Password = CryptoHelper.Crypto.HashPassword(user.Password);

                _repository.Add(user);
                await _repository.SaveChangesAsync();

                return CreatedAtAction("Register", new { id = user.Id }, user);
            }
            return BadRequest(user);
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

        private async Task<User> AuthenticateUser(string email, string password)
        {
            var users = await _repository.GetAllAsync();
            var user = users.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
