using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Validators
{
    public class SubscriptionLevelValidator : Validator<SubscriptionLevel>
    {
        private IRepository<SubscriptionLevel> _repository;
        public SubscriptionLevelValidator(IRepository<SubscriptionLevel> repository)
            : base()
        {
            _repository = repository;
        }
       
        public override List<Error> ValidateModel(SubscriptionLevel model, Type type)
        {
            if (type == Type.Post)
            {
                var existingSubscriptionLevels = _repository.Where(x => x.Name.ToLower() == model.Name.ToLower()).Any();

                if (existingSubscriptionLevels)
                {
                    errors.Add(new Error { ErrorMessage = "Subscription level already exists" });
                }
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new Error { ErrorMessage = "Subscription level name is required" });
            }

            if (model.MaxUsers <= 0)
            {
                errors.Add(new Error { ErrorMessage = "Max users cannot be less than 1" });
            }

            if (type == Type.Put)
            {
                if (model.Id < 1)
                {
                    errors.Add(new Error { ErrorMessage = "Id is required" });
                }
            }

            return errors;
        }
    }
}
