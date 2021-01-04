using DataAccess.Models;

namespace BackEnd.Validators
{
    public class SubscriptionLevelValidator : IValidator<SubscriptionLevel>
    {
        public bool Valid(SubscriptionLevel type)
        {
            return (!string.IsNullOrEmpty(type.Name) && type.MaxUsers > 0);

        }
    }
}
