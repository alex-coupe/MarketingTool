using Api.Helpers;
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
            if (HttpContext.User.HasClaim(claim => claim.Type == "Email").Equals("acoupe@gmail.com"))
            {
                templates = await _templateRepository.GetAllAsync();
            }

            var clientId = UserHelper.GetClientId(HttpContext.User.Claims);
            templates = await _templateRepository.GetAllAsync(x => x.ClientId == clientId);

            return Ok(templates);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Template>>> GetTemplate(int id)
        {
            var clientId = UserHelper.GetClientId(HttpContext.User.Claims);
            var template = await _templateRepository.GetAsync(x => x.ClientId == clientId,id);
            
            if (template != null)
                return Ok(template);
           
            return NotFound();
        }
    }
}
