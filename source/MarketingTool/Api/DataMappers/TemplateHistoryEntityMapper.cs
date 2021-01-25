using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class TemplateHistoryEntityMapper
    {
        public static void MapCollection(this IEnumerable<TemplateHistory> inVal, out TemplateHistoryViewModel outVal)
        {
            outVal = new TemplateHistoryViewModel();
            outVal.TemplateName = inVal.First().Template.Name;
            outVal.TemplateHistory = inVal.Select(x => new TemplateHistoryItemViewModel
            {
               Content = x.Content,
               ModifiedDate = x.ModifiedDate,
               ModifyingUser = $"{x.ModifyingUser?.FirstName} {x.ModifyingUser?.LastName}",
               Protected = x.Protected,
               Version = x.Version
            }).ToList();
        }
    }
}
