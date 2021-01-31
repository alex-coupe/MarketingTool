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
    public class CampaignsController : ControllerBase
    {
        private IRepository<Campaign> _campaignRepository;
        public CampaignsController(IRepository<Campaign> repository)
        {
            _campaignRepository = repository;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampaignViewModel>>> GetCampaigns()
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var campaigns = await _campaignRepository.GetAllAsync(x => x.ClientId == clientId);
            campaigns.Map(out List<CampaignViewModel> viewModel);
            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CampaignViewModel>> GetCampaign(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var campaign = await _campaignRepository.GetAsync(x => x.ClientId == clientId, id);

            if (campaign != null)
            {
                campaign.Map(out CampaignViewModel viewModel);
                return Ok(viewModel);
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Campaign>> PostCampaign(CampaignViewModel viewModel)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            viewModel.Map(out Campaign campaign);
            campaign.ClientId = clientId;
            CampaignValidator _validator = new CampaignValidator(clientId);
            var validationResult = await _validator.ValidateAsync(campaign);
            if (validationResult.IsValid)
            {
                _campaignRepository.Add(campaign);

                await _campaignRepository.SaveChangesAsync();
                return CreatedAtAction("PostCampaign", new { id = campaign.Id }, campaign);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Campaign>> PutCampaign(CampaignViewModel viewModel)
        {
            var campaign = await _campaignRepository.GetAsync(viewModel.Id);
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            viewModel.MapEdit(ref campaign);
            campaign.ClientId = clientId;
            CampaignValidator _validator = new CampaignValidator(clientId);
            var validationResult = await _validator.ValidateAsync(campaign);
            if (validationResult.IsValid)
            {
                _campaignRepository.Edit(campaign);
                await _campaignRepository.SaveChangesAsync();

                return Ok(viewModel);
            }

            return BadRequest(validationResult.Errors);
        }



        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCampaign(int id)
        {
            _campaignRepository.Remove(id);

            await _campaignRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
