using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User
    {

        [Key]
        public int Id { get; init; }

        [Required]
        public int ClientId { get; set; }

        [Required]

        public string FirstName { get; set; }

        [Required]
      
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        public bool Admin { get; set; }


        public bool Archived { get; set; } = false;


    }
}
