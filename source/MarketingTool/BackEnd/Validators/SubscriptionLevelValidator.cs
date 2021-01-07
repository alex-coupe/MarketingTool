using DataAccess.Models;

namespace BackEnd.Validators
{
    public class SubscriptionLevelValidator : IValidator<SubscriptionLevel>
    {
        public bool Valid(SubscriptionLevel model)
        {
            return (!string.IsNullOrEmpty(model.Name) && model.MaxUsers > 0);

        }
    }
}
