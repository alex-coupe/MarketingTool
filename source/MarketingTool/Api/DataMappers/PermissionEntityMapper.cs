using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class PermissionEntityMapper
    {

        public static IEnumerable<PermissionViewModel> Map(this ICollection<UserPermission> userPermissions)
        {
            return userPermissions.Select(x => new PermissionViewModel
            {
                Id = x.Permission.Id,
                Name = x.Permission.Name,
                Description = x.Permission.Description
            }).ToList();
        }
    }
}
