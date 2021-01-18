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
    public class RecipientValidator : AbstractValidator<Recipient>
    {
        private RecipientSchema _schema;
        public RecipientValidator(RecipientSchema schema)
        {
            _schema = schema;
            

            RuleFor(x => x.ClientId).Equal(schema.ClientId).WithMessage("Can't attach recipient to client other than your own");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email address is invalid");
            RuleFor(x => x.SchemaValues).Must(x => ValuesCountMatchesSchema(x.ToString())).WithMessage("Values provided does not match schema");
        }

        protected override bool PreValidate(ValidationContext<Recipient> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("", "Please ensure a model was supplied."));
                return false;
            }

            if (_schema == null)
            {
                result.Errors.Add(new ValidationFailure("", "Cannot create a recipient without a schema"));
                return false;
            }
            return true;
        }

        private bool ValuesCountMatchesSchema(string recipientSchema)
        {
            bool matches = true;
            JObject schema = (JObject)JsonConvert.DeserializeObject(_schema.Schema.ToString());
            JObject values = (JObject)JsonConvert.DeserializeObject(recipientSchema);

            var schemaProperties = schema.Properties();
            var recipientValues = values.Properties();

            if (schema.Count != values.Count)
                return false;

            for (int i = 0; i != schemaProperties.Count(); ++i)
            {
                var schemaValue = schemaProperties.ElementAt(i).Value.ToString();
                var recipientValue = recipientValues.ElementAt(i).Value.ToString();

                if (schemaProperties.ElementAt(i).Name != recipientValues.ElementAt(i).Name)
                    matches = false;

                if (schemaValue == "Number" && !double.TryParse(recipientValue, out _))
                    matches = false;

                if (schemaValue == "Checkbox" && !bool.TryParse(recipientValue, out _))
                    matches = false;
            }

            return matches;
        }
    }
}
