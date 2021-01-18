using DataAccess.Models;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class TemplateSynonymValidator : AbstractValidator<TemplateSynonym>
    {
        private RecipientSchema _schema;
        public TemplateSynonymValidator(RecipientSchema schema)
        {
            _schema = schema;
            RuleFor(x => x.Key).NotNull().NotEmpty().WithMessage("Values is not contained in your schema");
            RuleFor(x => x.Value).Must(x => ValueContainedInSchema(x.ToString())).WithMessage("Values is not contained in your schema");
            RuleFor(x => x.ClientId).Equal(schema.ClientId).WithMessage("Can't attach synonym to client other than your own");
        }

        protected override bool PreValidate(ValidationContext<TemplateSynonym> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "Please ensure a model was supplied."));
                return false;
            }

            if (_schema == null)
            {
                result.Errors.Add(new ValidationFailure("", "Cannot create a synonym without a schema"));
                return false;
            }
            return true;
        }

        private bool ValueContainedInSchema(string value)
        {
            JObject schema = (JObject)JsonConvert.DeserializeObject(_schema.Schema.ToString());
            var schemaProperties = schema.Properties();
            for (int i = 0; i != schemaProperties.Count(); ++i)
            {
               
                if (schemaProperties.ElementAt(i).Name == value)
                    return true;

            }
            return false;
        }
    }
}
