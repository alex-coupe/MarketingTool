using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEnd.Validators
{
    public class UserValidator : IValidator<User>
    {
        public bool Valid(User model)
        {
            return (model.ClientId > 0 && !string.IsNullOrEmpty(model.FirstName) && !string.IsNullOrEmpty(model.LastName)
                && !string.IsNullOrEmpty(model.Password) && Regex.IsMatch(model.EmailAddress,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }
    }
}
