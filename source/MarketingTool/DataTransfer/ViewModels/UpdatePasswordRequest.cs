using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    /// <summary>
    /// A data type to represent the changing of a password following a password reset request
    /// </summary>
    public class UpdatePasswordRequest
    {
        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}
