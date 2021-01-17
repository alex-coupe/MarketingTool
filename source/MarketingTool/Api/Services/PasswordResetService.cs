using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class PasswordResetService : BackgroundService
    {
        IRepository<PasswordReset> _repository;
        EmailService _emailService;
        public PasswordResetService(IRepository<PasswordReset> repository, EmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async void ProcessQueue()
        {
            var resets = _repository.Where(x => x.ResetSent == false).ToList();

            foreach (var reset in resets)
            {
                var resettingUser = reset.User;
                _emailService.Send(new MailAddress(reset.EmailAddress), new MailMessage
                {
                    IsBodyHtml = true,
                    Subject = "Your Password Reset Request",
                    Body = $"Hi {resettingUser.FirstName}, You've requested a password reset on your blabla.com account " +
                    $"Click <a href='https://localhost:44319/resetpassword/{reset.Token}>Here</a> To reset "
                });
                reset.ResetSent = true;
                await _repository.SaveChangesAsync();
            }
            
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    ProcessQueue();
                    await Task.Delay(TimeSpan.FromSeconds(300), token);
                }
            }, token);

        }

        public async Task Push(string email)
        {
            var Token = AuthHelper.GenerateToken();

            _repository.Add(new PasswordReset
            {
                EmailAddress = email,
                Token = Token,
                DateCreated = DateTime.Now
            });
            await _repository.SaveChangesAsync();

        }

        
    }
}
