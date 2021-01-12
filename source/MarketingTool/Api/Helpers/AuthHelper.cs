using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class AuthHelper
    {
        public static string GenerateToken()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 50)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
