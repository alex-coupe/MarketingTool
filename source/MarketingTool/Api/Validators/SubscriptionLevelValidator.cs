using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class SubscriptionLevelValidator : AbstractValidator<SubscriptionLevel>
    {
        private IRepository<SubscriptionLevel> _repository;
        public SubscriptionLevelValidator(IRepository<SubscriptionLevel> repository)
            : base()
        {
            _repository = repository;

            RuleFor(sl => sl.MaxUsers).GreaterThan(0).WithMessage("Max users must be more than 0");
            RuleFor(sl => sl.Name).NotNull().NotEmpty().WithMessage("Name must be filled out");
            RuleFor(x => x.Name).MustAsync(async (name, cancellation) => {
                bool exists = await IsDuplicate(name);
                return !exists;
            }).WithMessage("A subscription level with that name already exists");
        }

        private async Task<bool> IsDuplicate(string name)
        {
            var subLevels = await _repository.GetAllAsync();
            return subLevels.Any(x => x.Name.ToLower() == name?.ToLower());
        }


       
        
    }
}
