using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.DataTransferObjects
{
    public class UserDTO
    {
        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsArchived { get; set; }

        public int UserId { get; set; }

        public int ClientId { get; set; }
    }
}
