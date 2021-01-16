using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public static int GetClientId(IEnumerable<Claim> claims)
        {
            return int.Parse(claims.Where(c => c.Type == "ClientId").FirstOrDefault().Value);
        }

        public static int GetUserId(IEnumerable<Claim> claims)
        {
            return int.Parse(claims.Where(c => c.Type == "UserId").FirstOrDefault().Value);
        }

        public static bool CheckIfAdmin(IEnumerable<Claim> claims)
        {
            return bool.Parse(claims.Where(c => c.Type == "IsAdmin").FirstOrDefault().Value);
        }


    }
}
