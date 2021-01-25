using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class TemplateEntityMapper 
    {
        public static void Map(this Template inVal, out TemplateViewModel outVal)
        {
                outVal = new TemplateViewModel{
                    Name = inVal.Name,
                    Content = inVal.Content,
                    Version = inVal.Version,
                    CreatedDate = inVal.CreatedDate,
                    Id = inVal.Id,
                    Protected = inVal.Protected,
                    ModifiedDate = inVal.ModifiedDate,
                    ModifyingUser = $"{inVal.ModifyingUser?.FirstName} {inVal.ModifyingUser?.LastName}",
                    CreatingUser = $"{inVal.CreatingUser.FirstName} {inVal.CreatingUser.LastName}",
                };
        }

        public static void MapCollection(this IEnumerable<Template> inVal, out List<TemplateViewModel> outVal)
        {
            outVal = new List<TemplateViewModel>();
            inVal.Select(x => new TemplateViewModel
            {
                Name = x.Name,
                Content = x.Content,
                Version = x.Version,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Protected = x.Protected,
                ModifiedDate = x.ModifiedDate,
                ModifyingUser = $"{x.ModifyingUser?.FirstName} {x.ModifyingUser?.LastName}",
                CreatingUser = $"{x.CreatingUser.FirstName} {x.CreatingUser.LastName}",
            }).ToList();
        }
    }
}
