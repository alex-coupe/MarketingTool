using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataTransfer.ViewModels
{
    /// <summary>
    /// Represents a registration request from a user
    /// </summary>
    public class RegisterRequest
    {
        [Required]
        public string ClientName { get; set; }

        [Required]
        public int SubscriptionLevel { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
