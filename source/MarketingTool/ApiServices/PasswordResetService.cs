using DataAccess.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var token = GenerateToken();

            _repository.Add(new PasswordReset
            {
                EmailAddress = email,
                Token = token
            });
            await _repository.SaveChangesAsync();
            

            //issue email
            
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
