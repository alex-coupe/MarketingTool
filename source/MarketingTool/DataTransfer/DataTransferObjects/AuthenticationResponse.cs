using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.DataTransferObjects
{
    public class AuthenticationResponse
    { 
        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
