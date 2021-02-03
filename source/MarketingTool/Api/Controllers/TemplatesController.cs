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
using System.Security.Claims;
using System.Threading.Tasks;
using Api.DataMappers;

namespace Api.Controllers
{
    [Authorize(Policy = "NotArchived")]
    [Route("api/[controller]")]
    [ApiController]
    public class TemplatesController : ControllerBase
    {
        private IRepository<Template> _templateRepository;
        public TemplatesController(IRepository<Template> templateRepository)
        {
            _templateRepository = templateRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateViewModel>>> GetTemplates()
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var templates = await _templateRepository.GetAllAsync(x => x.ClientId == clientId, new string[] {"CreatingUser", "ModifyingUser" });

            templates.Map(out List<TemplateViewModel> viewModel);

            return Ok(viewModel);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateViewModel>> GetTemplate(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var template = await _templateRepository.GetAsync(x => x.ClientId == clientId && x.Id == id, new string[] { "CreatingUser", "ModifyingUser" });

            if (template != null)
            {
                template.Map(out TemplateViewModel viewModel);
                return Ok(viewModel);
            }
           
            return NotFound();
        }

        [Authorize(Policy = "AddTemplate")]
        [HttpPost]
        public async Task<ActionResult<Template>> PostTemplate(TemplateViewModel viewModel)
        {
            viewModel.Map(out Template template);
            template.ClientId = AuthHelper.GetClientId(HttpContext.User.Claims);

            TemplateValidator _validator = new TemplateValidator();
            var validationResult = await _validator.ValidateAsync(template);
            if (validationResult.IsValid)
            {
                _templateRepository.Add(template);

                await _templateRepository.SaveChangesAsync();
                return CreatedAtAction("PostTemplate", new { id = template.Id }, template);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize(Policy = "EditTemplate")]
        [HttpPut]
        public async Task<ActionResult<TemplateViewModel>> PutTemplate(TemplateViewModel viewModel,[FromServices]IRepository<TemplateHistory> _historyRepository)
        {
            var template = await _templateRepository.GetAsync(x => x.Id == viewModel.Id);
            template.MapToHistory(viewModel, out TemplateHistory history);
            _historyRepository.Add(history);

            viewModel.MapEdit(ref template);
            TemplateValidator _validator = new TemplateValidator();
            var validationResult = await _validator.ValidateAsync(template);
            if (validationResult.IsValid)
            {
                _templateRepository.Edit(template);
                await _templateRepository.SaveChangesAsync();
                await _historyRepository.SaveChangesAsync();

                return Ok(viewModel);
            }

            return BadRequest(validationResult.Errors);
        }

    }
}
