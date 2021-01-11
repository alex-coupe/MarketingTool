using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices
{
    public class PasswordResetService
    {
        IRepository<PasswordReset> _repository;
        EmailService _emailService;
        public PasswordResetService(IRepository<PasswordReset> repository, EmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }
        public async Task GenerateNewPasswordResetRequest(string email)
        {
            var Token = GenerateToken();

            _repository.Add(new PasswordReset
            {
                EmailAddress = email,
                Token = Token,
                DateCreated = DateTime.Now
            });
            await _repository.SaveChangesAsync();


            _emailService.Send(new MailAddress(email), new MailMessage
            {
                Subject = "Your Password Reset Request",
                IsBodyHtml = true,
                Body = $"<html><body><a href='https://localhost:44319/resetpassword/{Token}'>Reset Now</a></body></html>",
                BodyEncoding = Encoding.Unicode,
               
               
            });
            
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
