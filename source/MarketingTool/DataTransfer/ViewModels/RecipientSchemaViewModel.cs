using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.ViewModels
{
    public class RecipientSchemaViewModel
    {
        public IDictionary<string, string> Schema { get; set; }

        public void AddToSchema(RecipientSchemaItemViewModel model)
        {
            Schema.Add(model.Key,model.Value);
            model.Value = string.Empty;
            model.Key = string.Empty;
        }

        public void RemoveFromSchema(string key)
        {
            Schema.Remove(key);
        }

        
    }
}
