﻿using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
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
        IServiceScopeFactory _serviceScopeFactory;
        EmailService _emailService;
        private bool mutex = false;
        public PasswordResetService(IServiceScopeFactory serviceScopeFactory, EmailService emailService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _emailService = emailService;
        }

        public async Task ProcessQueue()
        {
            mutex = true;
            using (var scope = _serviceScopeFactory.CreateScope())
            {                
                var _repository = scope.ServiceProvider.GetService<IRepository<PasswordReset>>();

                var resets = await _repository.GetAllAsync(x => x.EmailSent == false);

                foreach (var reset in resets)
                {
                    
                    _emailService.Send(new MailAddress(reset.EmailAddress), new MailMessage
                    {
                        IsBodyHtml = true,
                        Subject = "Your Password Reset Request",
                        Body = $"Hi, You've requested a password reset on your blabla.com account " +
                        $"Click <a href='https://localhost:44319/resetpassword/{reset.Token}>Here</a> To reset "
                    });
                    reset.EmailSent = true;
                    await _repository.SaveChangesAsync();
                }
               
            }
            mutex = false;
        }

        protected override Task ExecuteAsync(CancellationToken token)
        {
            return Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (!mutex)
                    {
                        await ProcessQueue();
                        await Task.Delay(TimeSpan.FromSeconds(150), token);
                    }
                }
            }, token);

        }

        public async Task Push(string email, int userId)
        {
            var Token = AuthHelper.GenerateToken();
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _repository = scope.ServiceProvider.GetService<IRepository<PasswordReset>>();
                _repository.Add(new PasswordReset
                {
                    EmailAddress = email,
                    Token = Token,
                    DateCreated = DateTime.Now,
                    UserId = userId
                });
                await _repository.SaveChangesAsync();
            }
        }

        
    }
}
