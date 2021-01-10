using ApiServices;
using DataTransfer.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PasswordResetController : ControllerBase
    {
        private PasswordResetService _passwordService;
        public PasswordResetController(PasswordResetService service)
        {
            _passwordService = service;
        }    

        [Route("resetpassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody]PasswordResetRequest request)
        {
            await _passwordService.GenerateNewPasswordResetRequest(request.Email);
            return Ok();
        }
    }
}
