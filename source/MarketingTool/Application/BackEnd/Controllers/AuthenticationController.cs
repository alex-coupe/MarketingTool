using DataAccess.Models;
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
        private readonly DatabaseContext _context;

        public AuthenticationController(IConfiguration config, DatabaseContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public ActionResult Login(string email, string password)
        {
            var user = AuthenticateUser(email, password);

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
            user.Password = CryptoHelper.Crypto.HashPassword(user.Password);
           
            _context.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Register", new { id = user.Id }, user);
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
            var hash = CryptoHelper.Crypto.HashPassword(password);
            var user = _context.Users.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null && CryptoHelper.Crypto.VerifyHashedPassword(user.Password, password))
            {
                return user;
            }
            return null;
        }
    }
}
