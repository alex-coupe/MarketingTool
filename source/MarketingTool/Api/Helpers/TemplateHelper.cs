using DataAccess.Models;
using DataAccess.Repositories;
using System.Linq;

namespace Api.Helpers
{
    public static class TemplateHelper
    {
        
        public static string ConvertSynonymsToValues(Template template, IRepository<TemplateSynonym> _synonymsRepository)
        {
            var finalTemplate = string.Empty;
            var synonyms = _synonymsRepository.Where(x => x.ClientId == template.ClientId).ToList();


            return finalTemplate;
        }
    }
}
