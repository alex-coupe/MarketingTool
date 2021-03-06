﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class PasswordReset
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string Token { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User {get; set;}
   
        public bool EmailSent { get; set; }
    }
}
