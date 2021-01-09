﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransfer.DataTransferObjects
{
    public class AuthenticationResponse
    { 
        public string TokenType { get; set; }

        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public string Name { get; set; }

        public bool isAdmin { get; set; }

        public bool isArchived { get; set; }

        public int UserId { get; set; }

        public int ClientId { get; set; }
    }
}