﻿using Api.Helpers;
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
            var list = await _listRepository.GetAsync(x => x.ClientId == clientId, id);

            if (list != null)
                return Ok(list);

            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<List>> PostList(List list)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
           
            ListValidator _validator = new ListValidator(clientId);
            var validationResult = await _validator.ValidateAsync(list);
            if (validationResult.IsValid)
            {
                _listRepository.Add(list);

                await _listRepository.SaveChangesAsync();
                return CreatedAtAction("PostList", new { id = list.Id }, list);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<List>> PutList(List list)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);

            ListValidator _validator = new ListValidator(clientId);
            var validationResult = await _validator.ValidateAsync(list);
            if (validationResult.IsValid)
            {
                _listRepository.Edit(list);
                await _listRepository.SaveChangesAsync();

                return Ok(list);
            }

            return BadRequest(validationResult.Errors);
        }



        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteList(int id)
        {
            _listRepository.Remove(id);

            await _listRepository.SaveChangesAsync();

            return NoContent();
        }


    }
}
