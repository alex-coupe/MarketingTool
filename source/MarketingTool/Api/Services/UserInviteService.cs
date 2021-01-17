using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserInviteService  : BackgroundService
    {
        IServiceScopeFactory _serviceScopeFactory;
        EmailService _emailService;
      
        public UserInviteService(IServiceScopeFactory serviceScopeFactory, EmailService emailService)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _emailService = emailService;
        }
        public async Task Push(string emailAddress, int invitingUserId)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userInviteRepository = scope.ServiceProvider.GetService<IRepository<UserInvite>>();
                _userInviteRepository.Add(new UserInvite
                {
                    EmailAddress = emailAddress,
                    Token = AuthHelper.GenerateToken(),
                    InvitingUserId = invitingUserId
                });

                await _userInviteRepository.SaveChangesAsync();
            }
           
        }

      
        protected override Task ExecuteAsync(CancellationToken token)
        {
             return Task.Run(async () =>
             {
                while (!token.IsCancellationRequested)
                {
                    ProcessQueue();
                    await Task.Delay(TimeSpan.FromSeconds(100), token);
                }
             }, token);

        }

        public async void ProcessQueue()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var _userInviteRepository = scope.ServiceProvider.GetService<IRepository<UserInvite>>();
                var _userRepository = scope.ServiceProvider.GetService<IRepository<User>>();
                var _clientRepository = scope.ServiceProvider.GetService<IRepository<Client>>();
                var invites = _userInviteRepository.Where(x => x.InviteSent == false).ToList();
              
                foreach (var invite in invites)
                {
                    var invitingUser = _userRepository.Where(x => x.Id == invite.InvitingUserId).FirstOrDefault();
                    var invitingClient = _clientRepository.Where(x => x.Id == invitingUser.ClientId).FirstOrDefault();
                    _emailService.Send(new System.Net.Mail.MailAddress(invite.EmailAddress), new System.Net.Mail.MailMessage
                    {
                        Subject = "You've been invited",
                        Body = $"You've been invited to join {invitingClient.Name}'s blabla.com account by {invitingUser.FirstName} {invitingUser.LastName}." +
                        $"Click here to join {invite.Token} "
                    });

                    invite.InviteSent = true;
                    await _userInviteRepository.SaveChangesAsync();
                }
            }
        }
    }
}
