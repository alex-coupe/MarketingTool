using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class EditTemplateValidator : AbstractValidator<Template>
    {
        public EditTemplateValidator()
             : base()
        {

                RuleFor(template => template.Id).NotEmpty().GreaterThan(0).WithMessage("Id must be more than 0");
                RuleFor(template => template.ClientId).NotEmpty().GreaterThan(0).WithMessage("Client Id must be more than 0");
                RuleFor(template => template.CreatedDate).NotNull().WithMessage("Created date is required");
                RuleFor(template => template.ModifiedDate).NotNull().WithMessage("Modified date is required");
                RuleFor(template => template.Name).NotNull().NotEmpty().WithMessage("Name is required");
                RuleFor(template => template.Content).NotNull().NotEmpty().WithMessage("Content is required");
                RuleFor(template => template.CreatorId).NotEmpty().GreaterThan(0).WithMessage("Creator Id must be more than 0");
                RuleFor(template => template.ModifierId).NotEmpty().GreaterThan(0).WithMessage("Modifier Id must be more than 0");
                RuleFor(template => template.Version).NotEmpty().GreaterThan(0).WithMessage("Version must be more than 0");
        
        }

    }
}
