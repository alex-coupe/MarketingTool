using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class UserHelper
    {
        public static int GetClientId(IEnumerable<Claim> claims)
        {
            return int.Parse(claims.Where(c => c.Type == "ClientId").FirstOrDefault().Value);
        }
    }
}
