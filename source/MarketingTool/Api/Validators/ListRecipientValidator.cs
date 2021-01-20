using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class ListRecipientValidator : AbstractValidator<ListRecipient>
    {
        public ListRecipientValidator()
        {
            RuleFor(x => x.ListId).GreaterThan(0);
            RuleFor(x => x.RecipientId).GreaterThan(0);
        }
    }
}
