using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class TemplateValidator : AbstractValidator<Template>
    {
        public TemplateValidator()
            : base()
        {

            RuleFor(template => template.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be more than 0");
            RuleFor(template => template.ClientId).NotEmpty().GreaterThan(0).WithMessage("Client Id must be more than 0");
            RuleFor(template => template.CreatedDate).NotNull().WithMessage("Created date is required");
            RuleFor(template => template.Name).NotNull().NotEmpty().WithMessage("Name is required");
            RuleFor(template => template.Content).NotNull().NotEmpty().WithMessage("Content is required");
            RuleFor(template => template.CreatorId).NotEmpty().GreaterThan(0).WithMessage("Creator Id must be more than 0");
            RuleFor(template => template.Protected).NotNull().WithMessage("Protected can't be null");
            RuleFor(template => template.Version).NotEmpty().GreaterThan(0).WithMessage("Version must be more than 0");
            RuleFor(template => template.ModifiedDate.Value).NotNull().Must(BeAValidDate).When(x => x.Id > 0).WithMessage("Modified date is required");
            RuleFor(template => template.ModifierId).NotEmpty().GreaterThan(0).When(x => x.Id > 0).WithMessage("Modifier Id must be more than 0");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
