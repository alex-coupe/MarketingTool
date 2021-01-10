using DataAccess.Models;
using DataAccess.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class EditClientValidator : AbstractValidator<Client>
    {
        
        public EditClientValidator()
            : base()
        {
            RuleFor(client => client.Id).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(client => client.Name).NotNull().NotEmpty();
            RuleFor(client => client.SubscriptionLevelId).GreaterThan(0);
        }

        
    }
}
