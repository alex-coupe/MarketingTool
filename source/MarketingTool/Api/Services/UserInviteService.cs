using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
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
        IRepository<UserInvite> _userInviteRepository;
        EmailService _emailService;
      
        public UserInviteService(IRepository<UserInvite> userInviterepository, EmailService emailService)
        {
            _userInviteRepository = userInviterepository;
            _emailService = emailService;
        }
        public async Task<int> Push(string emailAddress, int invitingUserId)
        {
            _userInviteRepository.Add(new UserInvite
            {
                EmailAddress = emailAddress,
                Token = AuthHelper.GenerateToken(),
                InvitingUserId = invitingUserId
            });

            var result = await _userInviteRepository.SaveChangesAsync();

            return result;
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

        public async void ProcessQueue()
        {
            var invites = _userInviteRepository.Where(x => x.InviteSent == false).ToList();

            foreach (var invite in invites)
            {
                var invitingUser = invite.InvitingUser;
                _emailService.Send(new System.Net.Mail.MailAddress(invite.EmailAddress), new System.Net.Mail.MailMessage
                {
                    Subject = "You've been invited",
                    Body = $"You've been invited to join {invitingUser.Client.Name}'s blabla.com account by {invitingUser.FirstName} {invitingUser.LastName}." +
                    $"Click here to join {invite.Token} "
                });

                invite.InviteSent = true;
                await _userInviteRepository.SaveChangesAsync();
            }
        }
    }
}
