using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class UserViewModel
    {
        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public string Name { get; set; }

        public int RoleId { get; set; }

        public bool IsArchived { get; set; }

        public int UserId { get; set; }

        public int ClientId { get; set; }

        public DateTime? LastLogin { get; set; }

        public ICollection<PermissionViewModel> Permissions {get; set;}
    }
}
