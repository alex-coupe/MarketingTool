using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd.Validators
{
    public class ClientValidator : Validator<Client>
    {
        private IRepository<Client> _repository;
        public ClientValidator(IRepository<Client> repository)
            : base()
        {
            _repository = repository;
        }
        public override List<Error> ValidateModel(Client model)
        {
            var existingClientsWithName = _repository.Where(x => x.Name.ToLower() == model.Name.ToLower()).Count();

            if (existingClientsWithName > 0)
            {
                {
                    errors.Add(new Error { ErrorMessage = "Client already exists" });
                }
            }

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
