using Api.Services;
using DataAccess.Models;
using DataAccess.Repositories;
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
        private IRepository<PasswordReset> _repository;
        public PasswordResetController(PasswordResetService service, IRepository<PasswordReset> repository)
        {
            _passwordService = service;
            _repository = repository;
        }    

        [Route("resetpassword")]
        [HttpPost]
        public async Task<ActionResult> IssueReset([FromBody]PasswordResetRequest request)
        {
            await _passwordService.GenerateNewPasswordResetRequest(request.Email);
            return Ok();
        }

        [Route("resetpassword/{Token}")]
        [HttpGet]
        public ActionResult<PasswordReset> GetResetRequest([FromRoute]string Token)
        {
            var request = _repository.ToList().Where(x => x.Token == Token).FirstOrDefault();

            return Ok(request);
        }
    }
}
