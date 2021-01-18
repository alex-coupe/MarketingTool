using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Api.Validators
{
    public class PostClientValidator : AbstractValidator<Client>
    {
        
        public PostClientValidator()
            : base()
        {
            RuleFor(client => client.Name).NotNull().NotEmpty();
            RuleFor(client => client.SubscriptionLevelId).GreaterThan(0);
            
        }       
    }
}
