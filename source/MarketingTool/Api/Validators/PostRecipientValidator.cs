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
    public class PostRecipientValidator : AbstractValidator<Recipient>
    {
        private RecipientSchema _schema;
        public PostRecipientValidator(RecipientSchema schema, int clientId)
        {
            _schema = schema;
            

            RuleFor(x => x.ClientId).Equal(clientId).WithMessage("Can't attach recipient to client other than your own");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Email address is invalid");
            RuleFor(x => x.SchemaValues).Must(x => ValuesCountMatchesSchema(x.ToString())).WithMessage("Values count is not equal to schema count");
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

        private bool ValuesCountMatchesSchema(string schemaValues)
        {
            bool matches = true;
            JObject schema = (JObject)JsonConvert.DeserializeObject(_schema.Schema.ToString());
            JObject values = (JObject)JsonConvert.DeserializeObject(schemaValues);

            var x = schema.Properties();
            var y = values.Properties();
            for(int i = 0; i != x.Count(); ++i)
            {
                var xvalue = x.ElementAt(i).Value.ToString();
                var yvalue = y.ElementAt(i).Value.ToString();

                if (x.ElementAt(i).Name != y.ElementAt(i).Name)
                    matches = false;

                if (xvalue == "Number" && !double.TryParse(yvalue, out _))
                    matches = false;

                if (xvalue == "Checkbox" && !bool.TryParse(yvalue, out _))
                    matches = false;
            }

            if (schema.Count != values.Count)
                matches = false;

            return matches;
        }
    }
}
