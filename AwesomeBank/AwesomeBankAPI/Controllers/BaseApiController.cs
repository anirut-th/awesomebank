using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AwesomeBankAPI.Models;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ICustomerService _customerService;
        public BaseApiController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        protected string CustomerIdentityEmail
        {
            get
            {
                return CustomerData.Email;
            }
        }

        protected Customer CustomerData
        {
            get
            {
                var authRequestHeader = HttpContext.Request.Headers["Authorization"];
                var token = authRequestHeader.ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var customer = _customerService.GetCustomerByEmail(email);
                return customer;
            }
        }
    }
}