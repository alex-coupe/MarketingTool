﻿using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BackEnd.Validators
{
    public class UserValidator : Validator<User>
    {
        public UserValidator()
        {
            errors = new List<Error>();
        }
        
        public override List<Error> ValidateModel(User model)
        {
            if (string.IsNullOrEmpty(model.EmailAddress) || !Regex.IsMatch(model.EmailAddress,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                errors.Add(new Error { ErrorMessage = "Email is not valid" });
            }

            if (model.ClientId <= 0)
            {
                errors.Add(new Error { ErrorMessage = "Client ID cannot be 0" });
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                errors.Add(new Error { ErrorMessage = "First name is required" });
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                errors.Add(new Error { ErrorMessage = "Last name is required" });
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                errors.Add(new Error { ErrorMessage = "Password is required" });
            }


            return errors;
        }
    }
}
