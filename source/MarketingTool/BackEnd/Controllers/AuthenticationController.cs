using BackEnd.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.DataTransferObjects;
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

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IRepository<Client> _clientRepository;
        private readonly IRepository<User> _userRepository;

        public AuthenticationController(IConfiguration config, IRepository<User> userRepository, IRepository<Client> clientRepository)
        {
            _config = config;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest credentials)
        {
            var user = await AuthenticateUser(credentials.Email, credentials.Password);

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
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            IValidator<User> _userValidator = new UserValidator();
            IValidator<Client> _clientValidator = new ClientValidator();

            Client client = new Client()
            {
                Name = request.ClientName,
                SubscriptionLevelId = request.SubscriptionLevel
            };
         
            if (_clientValidator.Valid(client) )
            {
                _clientRepository.Add(client);
                await _clientRepository.SaveChangesAsync();

                User user = new User()
                {
                    ClientId = client.Id,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = CryptoHelper.Crypto.HashPassword(request.Password),
                    EmailAddress = request.EmailAddress,
                    Admin = true,
                    Archived = false
                };

                if (_userValidator.Valid(user))
                {
                    _userRepository.Add(user);
                    await _userRepository.SaveChangesAsync();
                }

                return CreatedAtAction("Register", new { id = user.Id }, user);
            }
            return BadRequest();
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
            var users = await _userRepository.GetAllAsync();
            var user = users.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
