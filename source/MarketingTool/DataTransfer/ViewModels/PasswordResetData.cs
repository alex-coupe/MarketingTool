using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    /// <summary>
    /// Provides data about a specific reset request based on the token
    /// </summary>
    public class PasswordResetData
    {
        public bool IsTokenValid { get; set; }

        public string EmailAddress { get; set; }

        public int UserId { get; set; }
    }
}
