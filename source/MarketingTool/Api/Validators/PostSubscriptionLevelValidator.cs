using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Api.Validators
{
    public class PostSubscriptionLevelValidator : AbstractValidator<SubscriptionLevel>
    {
        private IRepository<SubscriptionLevel> _repository;
        public PostSubscriptionLevelValidator(IRepository<SubscriptionLevel> repository)
            : base()
        {
            _repository = repository;

            RuleFor(sl => sl.MaxUsers).GreaterThan(0).WithMessage("Max users must be more than 0");
            RuleFor(sl => sl.Name).NotNull().NotEmpty().WithMessage("Name must be filled out");
            RuleFor(sl => sl).Must(sl => !IsDuplicate(sl.Name)).WithMessage("A subscription level with that name already exists");
        }

        private bool IsDuplicate(string name)
        {
            return _repository.GetAll().Any(x => x.Name.ToLower() == name?.ToLower());
        }


       
        
    }
}
