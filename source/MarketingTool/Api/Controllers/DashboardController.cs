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
        public ActionResult<DashboardViewModel> GetDashboardInfo()
        {
            var viewModel = new DashboardViewModel();
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var campaigns = _campaignsRepository.Where(x => x.ClientId == clientId).ToList();
            viewModel.ListCount = _listRepository.Where(x => x.ClientId == clientId).Count();
            viewModel.RecipientCount = _recipientsRepository.Where(x => x.ClientId == clientId).Count();
            viewModel.TotalTemplates = _templatesRepository.Where(x => x.ClientId == clientId).Count();

            viewModel.TotalCampaigns = campaigns.Count();
            viewModel.ActiveCampaigns = campaigns.Where(x => x.IsActive).Count();

            return Ok(viewModel);
        }
    }
}
