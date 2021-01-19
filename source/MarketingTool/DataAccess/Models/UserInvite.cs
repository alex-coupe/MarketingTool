using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserInvite
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
      
        public int InvitingUserId { get; set; }

        [Required]
        public bool InviteSent { get; set; }
    }
}
