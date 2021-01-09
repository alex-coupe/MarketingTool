using DataAccess.Models;
using DataTransfer.ViewModels;
using System.Collections.Generic;

namespace BackEnd.Validators
{
    public class SubscriptionLevelValidator : Validator<SubscriptionLevel>
    {
        public SubscriptionLevelValidator()
        {
            errors = new List<Error>();
        }
        public bool Valid(SubscriptionLevel model)
        {
            return (!string.IsNullOrEmpty(model.Name) && model.MaxUsers > 0);

        }

        public override List<Error> ValidateModel(SubscriptionLevel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new Error { ErrorMessage = "Subscription level name is required" });
            }

            if (model.MaxUsers <= 0)
            {
                errors.Add(new Error { ErrorMessage = "Max users cannot be less than 1" });
            }

            return errors;
        }
    }
}
