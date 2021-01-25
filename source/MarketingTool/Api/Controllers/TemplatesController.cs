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

namespace Api.Controllers
{
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
            var templates = await _templateRepository.GetAllAsync(x => x.ClientId == clientId);

            var templatesCollection = templates.Select(x => new TemplateViewModel
            {
                Name = x.Name,
                Content = x.Content,
                Version = x.Version,
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                Protected = x.Protected,
                ModifiedDate = x.ModifiedDate,
                ModifyingUser = $"{x.ModifyingUser?.FirstName} {x.ModifyingUser?.LastName}",
                CreatingUser = $"{x.CreatingUser.FirstName} {x.CreatingUser.LastName}",
            }).ToList();

            return Ok(templatesCollection);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateViewModel>> GetTemplate(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var template = await _templateRepository.GetAsync(x => x.ClientId == clientId,id);

            var templateViewModel = new TemplateViewModel
            {
                Name = template.Name,
                Content = template.Content,
                Version = template.Version,
                CreatedDate = template.CreatedDate,
                Id = template.Id,
                Protected = template.Protected,
                ModifiedDate = template.ModifiedDate,
                ModifyingUser = $"{template.ModifyingUser?.FirstName} {template.ModifyingUser?.LastName}",
                CreatingUser = $"{template.CreatingUser.FirstName} {template.CreatingUser.LastName}",
            };
                                 
            if (template != null)
                return Ok(templateViewModel);
           
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Template>> PostTemplate(Template template)
        {
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

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Template>> PutTemplate(Template template)
        {
            TemplateValidator _validator = new TemplateValidator();
            var validationResult = await _validator.ValidateAsync(template);
            if (validationResult.IsValid)
            {
                _templateRepository.Edit(template);
                await _templateRepository.SaveChangesAsync();

                return Ok(template);
            }

            return BadRequest(validationResult.Errors);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> DeleteTemplate(int id)
        {
            _templateRepository.Remove(id);

            await _templateRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
