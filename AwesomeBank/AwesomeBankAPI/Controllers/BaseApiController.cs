using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected string CustomerIdentityEmail
        {
            get
            {
                var authRequestHeader = HttpContext.Request.Headers["Authorization"];
                var token = authRequestHeader.ToString().Replace("Bearer ", "");
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token) as JwtSecurityToken;
                var email = tokenS.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                return email;
            }
        }
    }
}