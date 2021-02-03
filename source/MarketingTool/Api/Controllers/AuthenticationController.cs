using Api.Services;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.Enums;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult> Login([FromBody] LoginViewModel credentials)
        {
            var user = await AuthenticateUser(credentials.EmailAddress, credentials.Password);

            if (user != null)
            {
                var token = GenerateJSONWebToken(user);
                var userResponse = new UserViewModel { TokenType = "Bearer", Token = new JwtSecurityTokenHandler().WriteToken(token), 
                    Expires = token.ValidTo, UserId = user.Id, ClientId = user.ClientId, RoleId=user.RoleId, IsArchived = user.Archived, Name = user.FirstName };
                return Ok(JsonSerializer.Serialize(userResponse));
            }

            return Unauthorized(new ErrorMessageViewModel { ErrorMessage = "Email Address or Password Incorrect" });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegistrationViewModel newRegistration, [FromServices] IRepository<Client> _clientRepository)
        {
            
            var client = new Client
            {
                Name = newRegistration.ClientName,
                SubscriptionLevelId = newRegistration.SubscriptionLevel
            };

            _clientRepository.Add(client);
            await _clientRepository.SaveChangesAsync();         

            if (client.Id > 0)
            {

                var user = new User
                {
                    FirstName = newRegistration.FirstName,
                    LastName = newRegistration.LastName,
                    RoleId = (int)RolesEnum.Founder,
                    Archived = false,
                    EmailAddress = newRegistration.EmailAddress,
                    Password = CryptoHelper.Crypto.HashPassword(newRegistration.Password),
                    ClientId = client.Id
                };

                 _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
               
               return await Login(new LoginViewModel { EmailAddress = user.EmailAddress, Password = newRegistration.Password });     
            }
            
            return BadRequest(new ErrorMessageViewModel { ErrorMessage = "Error Creating Client" });
        }

        [AllowAnonymous]
        [Route("ForgottenPassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody] EmailAddressViewModel request, [FromServices]PasswordResetService resetService)
        {
            var user = await _userRepository.GetAsync(x => x.EmailAddress == request.EmailAddress);

            if (user != null)
            {
                await resetService.Push(request.EmailAddress, user.Id);
               
                return Ok();
            }
            return NotFound(new ErrorMessageViewModel { ErrorMessage = "User Not Found" });
        }

        [AllowAnonymous]
        [Route("UpdatePassword")]
        [HttpPost]
        public async Task<ActionResult> UpdatePassword([FromBody] ResetPasswordViewModel request)
        {
            if (request.Password != request.ConfirmPassword)
                return BadRequest(new ErrorMessageViewModel { ErrorMessage = "Password and Password Confirmation Do Not Match" });

            var entries = await _passwordResetRepository.GetAllAsync(x => x.Token == request.Token);
            var resetEntry = entries.OrderByDescending(x => x.DateCreated).FirstOrDefault();

            if (resetEntry == null || resetEntry.DateCreated.AddMinutes(30) < DateTime.Now)
            {
                return BadRequest(new ErrorMessageViewModel { ErrorMessage = "Invalid Token" });
            }
            
            if (resetEntry.User != null)
            {
             
                resetEntry.User.Password = CryptoHelper.Crypto.HashPassword(request.Password);
                _passwordResetRepository.Remove(resetEntry.Id);
                await _userRepository.SaveChangesAsync();
                await _passwordResetRepository.SaveChangesAsync();
                //TODO send email confirming password change
                return Ok();
            }
            return BadRequest(new ErrorMessageViewModel  { ErrorMessage = "User Not Found From This Token" });
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
            var claims = new List<Claim>
            {
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("Archived", user.Archived.ToString()),
                new Claim("ClientId", user.ClientId.ToString()),
                new Claim("UserId", user.Id.ToString()),
            };

            foreach(var permission in user.Permissions)
            {
                claims.Add(new Claim("Permission", permission.PermissionId.ToString()));
            }
           

            var token = new JwtSecurityToken(null,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signingCredentials);

            return token;
        }

        private async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await _userRepository.GetAsync(x => x.EmailAddress == email, new string[] { "Permissions" });

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
