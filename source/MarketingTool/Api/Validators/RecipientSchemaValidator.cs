﻿using DataAccess.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class RecipientSchemaValidator : AbstractValidator<RecipientSchema>
    {
        public RecipientSchemaValidator()
        {
            RuleFor(schema => schema.ClientId).GreaterThan(0).WithMessage("Client id cannot be 0");
            RuleFor(schema => schema.Schema).NotEmpty().NotNull().WithMessage("Schema cannot be empty");
        }
    }
}