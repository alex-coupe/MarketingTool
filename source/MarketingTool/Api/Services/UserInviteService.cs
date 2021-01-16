using Api.Helpers;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public class UserInviteService 
    {
        IRepository<UserInvite> _userInviteRepository;
        public UserInviteService(IRepository<UserInvite> repository)
        {
            _userInviteRepository = repository;
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
    }
}
