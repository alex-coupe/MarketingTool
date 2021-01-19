using DataAccess.Models;
using DataAccess.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Api.Helpers
{
    public static class TemplateHelper
    {
        
        public static string ConvertSynonymsToValues(Template template, IRepository<TemplateSynonym> _synonymsRepository, Recipient recipient)
        {
            var finalTemplate = string.Empty;
            var synonyms = _synonymsRepository.Where(x => x.ClientId == template.ClientId).ToList();

            foreach(var synonym in synonyms)
            {
                finalTemplate = template.Content.Replace(synonym.Key, recipient.SchemaValues.Value<string>(synonym.Value));
            }
           

            return finalTemplate;
        }
    }
}
