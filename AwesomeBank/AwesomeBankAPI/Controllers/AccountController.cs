using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public ActionResult GetAccount(string id)
        {
            Guid _Id = Guid.Parse(id);
            var result = _accountService.GetAccount(_Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        //public ActionResult
    }
}