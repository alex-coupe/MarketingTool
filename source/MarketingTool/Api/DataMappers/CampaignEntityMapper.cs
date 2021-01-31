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

        public static void Map(this Campaign inVal, out CampaignViewModel outVal)
        {

            outVal  = new CampaignViewModel
            {
                Name = inVal.Name,
                Description = inVal.Description,
                CreatedDate = inVal.CreatedDate,
                Id = inVal.Id,
                ModifiedDate = inVal.ModifiedDate,
                ModifyingUser = $"{inVal.ModifyingUser?.FirstName} {inVal.ModifyingUser?.LastName}",
                CreatingUser = $"{inVal.CreatingUser.FirstName} {inVal.CreatingUser.LastName}",
                IsActive = inVal.IsActive,
                ListId = inVal.ListId,
                ProcessedTimestamp = inVal.ProcessedTimestamp,
                SendDate = inVal.SendDate,
                SenderEmail = inVal.SenderEmail,
                Subject = inVal.Subject,
                TemplateId = inVal.TemplateId

            };

        }

        public static void Map(this CampaignViewModel inVal, out Campaign outVal)
        {
            outVal = new Campaign
            {
                Name = inVal.Name,
                SendDate = inVal.SendDate,
                TemplateId = inVal.TemplateId,
                Subject = inVal.Subject,
                SenderEmail = inVal.SenderEmail,
                ListId = inVal.ListId,
                IsActive = inVal.IsActive,
                Id = inVal.Id,
                ModifierId = !string.IsNullOrEmpty(inVal.ModifyingUser) ? int.Parse(inVal.ModifyingUser) : null,
                Description = inVal.Description,
                CreatedDate = DateTime.Now,
                CreatorId = int.Parse(inVal.CreatingUser),
                ModifiedDate = inVal.ModifiedDate.HasValue ? inVal.ModifiedDate.Value : null
            };
        }

        public static void MapEdit(this CampaignViewModel inVal, ref Campaign outVal)
        {
            {
                outVal.Name = inVal.Name;
                outVal.SendDate = inVal.SendDate;
                outVal.TemplateId = inVal.TemplateId;
                outVal.Subject = inVal.Subject;
                outVal.SenderEmail = inVal.SenderEmail;
                outVal.ListId = inVal.ListId;
                outVal.IsActive = inVal.IsActive;
                outVal.ModifierId = !string.IsNullOrEmpty(inVal.ModifyingUser) ? int.Parse(inVal.ModifyingUser) : null;
                outVal.Description = inVal.Description;
                outVal.ModifiedDate = inVal.ModifiedDate.HasValue ? inVal.ModifiedDate.Value : null;

            };
        }
    }
}
