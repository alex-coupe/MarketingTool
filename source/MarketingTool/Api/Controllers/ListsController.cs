using Api.Helpers;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DataMappers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private IRepository<List> _listRepository;
        private IRepository<Campaign> _campaignRepository;
        private IRepository<ListRecipient> _listRecipientRepository;
        public ListsController(IRepository<List> listRepository, IRepository<Campaign> campaignRepository,
           IRepository<ListRecipient> listRecipientRepository)
        {
            _listRepository = listRepository;
            _campaignRepository = campaignRepository;
            _listRecipientRepository = listRecipientRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListViewModel>>> GetLists()
        {
          
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var lists = await _listRepository.GetAllAsync(x => x.ClientId == clientId);
            
            lists.Map(out List<ListViewModel> viewModel, _campaignRepository);

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var list = await _listRepository.GetAsync(x => x.ClientId == clientId && x.Id == id);

            if (list != null)
            {
                list.Map(out ListViewModel viewModel, _campaignRepository);
                return Ok(viewModel);
            }

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List>> PostList(ListViewModel viewModel)
        {
            viewModel.Map(out List list);
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            list.ClientId = clientId;        

            ListValidator _validator = new ListValidator(clientId);
            var validationResult = await _validator.ValidateAsync(list);
            if (validationResult.IsValid)
            {
                _listRepository.Add(list);
                           
                await _listRepository.SaveChangesAsync();

                foreach (var recipient in viewModel.Recipients)
                {
                    _listRecipientRepository.Add(new ListRecipient
                    {
                        RecipientId = recipient.Id,
                        ListId = list.Id
                    });
                }
                await _listRecipientRepository.SaveChangesAsync();
                return CreatedAtAction("PostList", new { id = list.Id }, list);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> PutList(ListViewModel viewModel)
        {
            var list = await _listRepository.GetAsync(viewModel.Id);
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var listRecipients = _listRecipientRepository.Where(x => x.ListId == list.Id).ToList();
            viewModel.MapEdit(ref list);
            
            ListValidator _validator = new ListValidator(clientId);
            var validationResult = await _validator.ValidateAsync(list);
            if (validationResult.IsValid)
            {
                _listRepository.Edit(list);

                foreach(var rec in listRecipients)
                {
                    if (!viewModel.Recipients.Any(x => x.Id == rec.RecipientId))
                    {
                        _listRecipientRepository.Remove(rec.Id);

                    }
                }

                foreach(var rec in viewModel.Recipients)
                {
                    if (!listRecipients.Any(x => x.ListId == list.Id && x.RecipientId == rec.Id))
                    {
                        _listRecipientRepository.Add(new ListRecipient
                        {
                            ListId = list.Id,
                            RecipientId = rec.Id
                        });

                    }
                }
                await _listRecipientRepository.SaveChangesAsync();
                await _listRepository.SaveChangesAsync();

                return Ok(viewModel);
            }

            return BadRequest(validationResult.Errors);
        }



        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteList(int id)
        {
            _listRepository.Remove(id);

            await _listRepository.SaveChangesAsync();

            return NoContent();
        }


    }
}
