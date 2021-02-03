using Api.Helpers;
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

namespace Api.Controllers
{
    [Authorize(Policy = "NotArchived")]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private IRepository<Campaign> _campaignsRepository;
        private IRepository<Template> _templatesRepository;
        private IRepository<List> _listRepository;
        private IRepository<Recipient> _recipientsRepository;
        public DashboardController(IRepository<Campaign> campaignsRepository, 
            IRepository<Template> templatesRepository,
            IRepository<List> listRepository,
            IRepository<Recipient> recipientRepository
            )
        {
            _campaignsRepository = campaignsRepository;
            _templatesRepository = templatesRepository;
            _listRepository = listRepository;
            _recipientsRepository = recipientRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DashboardViewModel>> GetDashboardInfo()
        {
            var viewModel = new DashboardViewModel();
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var campaigns = await _campaignsRepository.GetAllAsync(x => x.ClientId == clientId);
            var recipients = await _recipientsRepository.GetAllAsync(x => x.ClientId == clientId);
            var templates = await _templatesRepository.GetAllAsync(x => x.ClientId == clientId);
            var lists = await _listRepository.GetAllAsync(x => x.ClientId == clientId);
            viewModel.ListCount = lists.Count();
            viewModel.RecipientCount = recipients.Count();
            viewModel.TotalTemplates = templates.Count();

            viewModel.TotalCampaigns = campaigns.Count();
            viewModel.ActiveCampaigns = campaigns.Where(x => x.IsActive).Count();

            return Ok(viewModel);
        }
    }
}
