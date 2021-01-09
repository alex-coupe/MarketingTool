using DataAccess.Models;
using DataTransfer.ViewModels;
using System.Collections.Generic;

namespace BackEnd.Validators
{
    public class ClientValidator : Validator<Client>
    {
        public ClientValidator()
        {
            errors = new List<Error>();
        }
        public override List<Error> ValidateModel(Client model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                errors.Add(new Error { ErrorMessage = "Client name is required" });
            }

            if (model.SubscriptionLevelId <= 0)
            {
                errors.Add(new Error { ErrorMessage = "Subscription level id is required" });
            }
            return errors;
        }
    }
}
