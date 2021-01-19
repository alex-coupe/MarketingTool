using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class CampaignValidator : AbstractValidator<Campaign>
    {
        public CampaignValidator(int clientId)
        {
            //Maybe validate that the template exists and belongs to the client?

            RuleFor(x => x.ClientId).Equal(clientId).WithMessage("Can't attach campaign  to client other than your own");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Subject).NotNull().NotEmpty().WithMessage("Subject is required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.CreatorId).GreaterThan(0).WithMessage("Creator Id is required");
            RuleFor(x => x.TemplateId).GreaterThan(0).WithMessage("Template Id is required");
            RuleFor(x => x.CreatedDate).Must(BeAValidDate).WithMessage("Created date must be valid");
            RuleFor(x => x.ModifiedDate.Value).NotNull().When(x => x.Id > 0).Must(BeAValidDate).WithMessage("Modified date is required");
            RuleFor(x => x.ModifierId).NotEmpty().GreaterThan(0).When(x => x.Id > 0).WithMessage("Modifier Id must be more than 0");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
