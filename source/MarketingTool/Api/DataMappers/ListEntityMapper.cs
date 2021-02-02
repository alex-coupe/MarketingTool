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
        public static void Map(this IEnumerable<List> inVal, out List<ListViewModel> outVal)
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
                Recipients = x.ListRecipients.Select(lr => new RecipientViewModel
                {
                    EmailAddress = lr.Recipient.EmailAddress,
                    SchemaValues = lr.Recipient.SchemaValues.ToObject<Dictionary<string,string>>(),
                    Id = lr.RecipientId,
                    Notes = lr.Recipient.Notes
                }).ToList()
            }).ToList();
        }

        public static void Map(this List inVal, out ListViewModel outVal)
        {
            outVal = new ListViewModel
            {
                Name = inVal.Name,
                Description = inVal.Description,
                CreatedDate = inVal.CreatedDate,
                Id = inVal.Id,
                ModifiedDate = inVal.ModifiedDate,
                ModifyingUser = $"{inVal.ModifyingUser?.FirstName} {inVal.ModifyingUser?.LastName}",
                CreatingUser = $"{inVal.CreatingUser.FirstName} {inVal.CreatingUser.LastName}",
                Recipients = inVal.ListRecipients.Select(lr => new RecipientViewModel
                {
                    EmailAddress = lr.Recipient.EmailAddress,
                    SchemaValues = lr.Recipient.SchemaValues.ToObject<Dictionary<string, string>>(),
                    Id = lr.RecipientId,
                    Notes = lr.Recipient.Notes
                }).ToList()
            };
        }

        public static void MapEdit(this ListViewModel inVal, ref List outVal)
        {
            {
                outVal.Name = inVal.Name;
                outVal.Description = inVal.Description;
                outVal.ModifiedDate = DateTime.Now;
                outVal.ModifierId = int.Parse(inVal.ModifyingUser);
               
            };
        }

        public static void Map(this ListViewModel inVal, out List outVal)
        {
            outVal = new List
            {
                Name = inVal.Name,
                Id = inVal.Id,
                ModifierId = !string.IsNullOrEmpty(inVal.ModifyingUser) ? int.Parse(inVal.ModifyingUser) : null,
                Description = inVal.Description,
                CreatedDate = DateTime.Now,
                CreatorId = int.Parse(inVal.CreatingUser),
                ModifiedDate = inVal.ModifiedDate.HasValue ? inVal.ModifiedDate.Value : null
            };
        }

    }
}
