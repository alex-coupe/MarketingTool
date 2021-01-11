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

        [Route("resetpassword/{Token}")]
        [HttpGet]
        public ActionResult<PasswordResetData> GetResetRequest([FromRoute] string Token)
        {
            var request = _passwordResetRepository.ToList().Where(x => x.Token == Token).FirstOrDefault();

            var resetData = new PasswordResetData
            {
                EmailAddress = request.EmailAddress,
                IsTokenValid = request.DateCreated.AddMinutes(30) > DateTime.Now ? true : false,
                UserId = request.UserId
            };

            return Ok(resetData);
        }

        [Route("resetpasswordrequests")]
        [HttpGet]
        public async Task<ActionResult<PasswordResetData>> GetResetRequests()
        {
            var requests = await _passwordResetRepository.GetAllAsync();

            return Ok(requests);
        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody] PasswordResetRequest request)
        {
            var user = _userRepository.Where(x => x.EmailAddress == request.Email).FirstOrDefault();

            if (user != null)
            {
                //generating token here for test purposes
                var token = GenerateToken();

                _passwordResetRepository.Add(new PasswordReset
                {
                    EmailAddress = request.Email,
                    Token = token,
                    DateCreated = DateTime.Now,
                    UserId = user.Id
                });
                await _passwordResetRepository.SaveChangesAsync();

                //placeholder
                var email = new EmailService();
                email.Send(new System.Net.Mail.MailAddress(request.Email), new System.Net.Mail.MailMessage { Subject = "token", Body = token });
                return Ok();
            }
            return NotFound();
        }

        [Route("updatepassword")]
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
            var user = _userRepository.Where(x => x.Id == request.UserId && x.EmailAddress == request.EmailAddress).FirstOrDefault();

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

        [Route("removepasswordreset")]
        [HttpDelete]
        public async Task<ActionResult> RemoveRequest(int id)
        {
            _passwordResetRepository.Remove(id);
            await _passwordResetRepository.SaveChangesAsync();
            return NoContent();
        }

        private string GenerateToken()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 50)
              .Select(s => s[random.Next(s.Length)]).ToArray());
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
