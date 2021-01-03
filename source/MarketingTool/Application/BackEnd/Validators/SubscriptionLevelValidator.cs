using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
