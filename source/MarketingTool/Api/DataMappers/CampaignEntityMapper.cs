using DataAccess.Models;
using DataTransfer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class CampaignEntityMapper
    {
        public static void Map(this IEnumerable<Campaign> inVal, out List<CampaignViewModel> outVal)
        {

            outVal = inVal.Select(x => new CampaignViewModel
            {
                Name = x.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                ModifiedDate = x.ModifiedDate,
                ModifyingUser = $"{x.ModifyingUser?.FirstName} {x.ModifyingUser?.LastName}",
                CreatingUser = $"{x.CreatingUser.FirstName} {x.CreatingUser.LastName}",
                IsActive = x.IsActive,
                ListId = x.ListId,
                ProcessedTimestamp = x.ProcessedTimestamp,
                SendDate = x.SendDate,
                SenderEmail = x.SenderEmail,
                Subject = x.Subject,
                TemplateId = x.TemplateId               
               
            }).ToList();
        }
    }
}
