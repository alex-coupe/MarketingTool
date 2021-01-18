﻿using Api.Helpers;
using Api.Validators;
using DataAccess.Models;
using DataAccess.Repositories;
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
        public async Task<ActionResult<IEnumerable<Template>>> GetTemplates()
        {
            IEnumerable<Template> templates;
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email" && claim.Value == "acoupe@gmail.com"))
            {
                templates = await _templateRepository.GetAllAsync();
            }

            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            templates = await _templateRepository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(templates);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Template>> GetTemplate(int id)
        {
            var clientId = AuthHelper.GetClientId(HttpContext.User.Claims);
            var template = await _templateRepository.GetAsync(x => x.ClientId == clientId,id);
            
            if (template != null)
                return Ok(template);
           
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Template>> PostTemplate(Template template)
        {
            PostTemplateValidator _validator = new PostTemplateValidator();
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
            EditTemplateValidator _validator = new EditTemplateValidator();
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
