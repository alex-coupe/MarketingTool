using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.DataMappers
{
    public static class ListEntityMapper
    {
        public static void Map(this IEnumerable<List> inVal, out List<ListViewModel> outVal, IRepository<Campaign> _campaignRepository)
        {

            outVal = inVal.Select(x => new ListViewModel
            {
                Name = x.Name,
                Description = x.Description,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                ModifiedDate = x.ModifiedDate,
                ModifyingUser = $"{x.ModifyingUser?.FirstName} {x.ModifyingUser?.LastName}",
                CreatingUser = $"{x.CreatingUser.FirstName} {x.CreatingUser.LastName}",
                CampaignCount = _campaignRepository.Where(camp => camp.ListId == x.Id).Count(),
                Recipients = x.ListRecipients.Select(bla => new RecipientViewModel
                {
                    EmailAddress = bla.Recipient.EmailAddress,
                    SchemaValues = bla.Recipient.SchemaValues.ToObject<Dictionary<string,string>>(),
                    Id = bla.Id,
                    Notes = bla.Recipient.Notes
                }).ToList()
            }).ToList();
        }
    }
}
