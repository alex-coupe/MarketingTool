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
using Api.DataMappers;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateHistoryController : ControllerBase
    {
        private IRepository<TemplateHistory> _repository;

        public TemplateHistoryController(IRepository<TemplateHistory> repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TemplateHistoryViewModel>> GetTemplateHistory(int templateId)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var history = await _repository.GetAllAsync(x => x.TemplateId == templateId);
            history.MapCollection(out TemplateHistoryViewModel viewModel);
            return Ok(viewModel);
        }
    }
}
