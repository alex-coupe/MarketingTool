using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validators
{
    public class ClientValidator : IValidator<Client>
    {
        public bool Valid(Client type)
        {
            return (!string.IsNullOrEmpty(type.AddressOne) && !string.IsNullOrEmpty(type.City) && !string.IsNullOrEmpty(type.Name)
                && !string.IsNullOrEmpty(type.State) && !string.IsNullOrEmpty(type.PostalCode) && type.SubscriptionLevelId > 0
                && !string.IsNullOrEmpty(type.TelephoneOne));
        }
    }
}
