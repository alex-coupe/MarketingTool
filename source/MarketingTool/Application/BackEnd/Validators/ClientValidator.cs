using DataAccess.Models;

namespace BackEnd.Validators
{
    public class ClientValidator : IValidator<Client>
    {
        public bool Valid(Client type)
        {
            return  (!string.IsNullOrEmpty(type.Name) && type.SubscriptionLevelId > 0);
        }
    }
}
