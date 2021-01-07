using DataAccess.Models;

namespace BackEnd.Validators
{
    public class ClientValidator : IValidator<Client>
    {
        public bool Valid(Client model)
        {
            return  (!string.IsNullOrEmpty(model.Name) && model.SubscriptionLevelId > 0);
        }
    }
}
