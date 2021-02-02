using DataAccess.Models;
using DataAccess.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public static class TemplateHelper
    {
        
        public static async Task<string> ConvertSynonymsToValuesAsync(Template template, IRepository<TemplateSynonym> _synonymsRepository, Recipient recipient)
        {
            var finalTemplate = string.Empty;
            var synonyms = await _synonymsRepository.GetAllAsync(x => x.ClientId == template.ClientId);
          
            foreach(var synonym in synonyms)
            {
                if (string.IsNullOrEmpty(finalTemplate))
                {
                    finalTemplate = template.Content.Replace(synonym.Key, recipient.SchemaValues.Value<string>(synonym.Value));
                }
                else
                {
                    finalTemplate = finalTemplate.Replace(synonym.Key, recipient.SchemaValues.Value<string>(synonym.Value));
                }
            }
           

            return finalTemplate;
        }
    }
}
