using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class UserEntityMapper
    {
        public static void Map(this IEnumerable<User> inVal, out List<UserViewModel> outVal)
        {

            outVal = inVal.Select(x => new UserViewModel
            {
                Name = $"{x.FirstName} {x.LastName}",
                UserId = x.Id,
                ClientId = x.ClientId,
                IsArchived = x.Archived,
                RoleId = x.RoleId,
                Permissions = x.Permissions,
                LastLogin = x.LastLogin
               
            }).ToList();
        }

        public static void Map(this User inVal, out UserViewModel outVal)
        {

            outVal = new UserViewModel
            {
                Name = $"{inVal.FirstName} {inVal.LastName}",
                UserId = inVal.Id,
                ClientId = inVal.ClientId,
                IsArchived = inVal.Archived,
                RoleId = inVal.RoleId,
                LastLogin = inVal.LastLogin

            };
        }
    }
}
