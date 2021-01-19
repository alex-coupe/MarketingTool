﻿using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private IRepository<User> _repository;
        public UserValidator(IRepository<User> repository)
            : base()
        {
            _repository = repository;
            RuleFor(user => user.EmailAddress).EmailAddress().NotEmpty().NotNull().WithMessage("Invalid email address entered");
            RuleFor(user => user.ClientId).NotEmpty().NotNull().GreaterThan(0).WithMessage("Client id is required");
            RuleFor(user => user.FirstName).NotNull().NotEmpty().WithMessage("First name is required");
            RuleFor(user => user.LastName).NotNull().NotEmpty().WithMessage("Last name is required");
            RuleFor(user => user.Password).NotNull().NotEmpty().WithMessage("Password is required");
            RuleFor(user => user).Must(user => !IsDuplicate(user.EmailAddress)).WithMessage("An account with that email address already exists");
        }

        private bool IsDuplicate(string email)
        {
            return _repository.GetAll().Any(x => x.EmailAddress.ToLower() == email?.ToLower());
        }
    }
}