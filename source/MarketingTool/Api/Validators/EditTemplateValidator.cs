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
        {

        }
    }
}
