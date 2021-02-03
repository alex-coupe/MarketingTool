using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UI.Helpers
{
    public static class ExtensionMethods
    {
       
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
        
        public static string ConvertRoleIdToString(int RoleId)
        {
            switch(RoleId)
            {
                case 1:return "Root User";
                case 2:return "Founding User";
                case 3: return "Admin User";
                case 4: return "Standard User";
                default: return "Unknown";

            }
        }

    }
}
