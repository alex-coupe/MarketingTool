using Api.Services;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private IRepository<PasswordReset> _passwordResetRepository;
        IRepository<User> _userRepository;
        public PasswordResetController(IRepository<PasswordReset> passwordResetRepository, IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _passwordResetRepository = passwordResetRepository;
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

        [Route("resetpassword/requests")]
        [HttpGet]
        public async Task<ActionResult<PasswordResetData>> GetResetRequests()
        {
            var requests = await _passwordResetRepository.GetAllAsync();

            return Ok(requests);
        }

        [Route("resetpassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody]PasswordResetRequest request)
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
                email.Send(new System.Net.Mail.MailAddress(request.Email), new System.Net.Mail.MailMessage {Subject = "token", Body = token });
                return Ok();
            }
            return NotFound();
        }

        [Route("updatepassword")]
        [HttpPost]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request )
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

        [Route("resetpassword/removerequest")]
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
    }
}
