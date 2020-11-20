using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("token")]
        public ActionResult token()
        {
            try
            {
                var authRequestHeader = HttpContext.Request.Headers["Authorization"];
                var authBasicToken = Encoding.UTF8.GetString(Convert.FromBase64String(authRequestHeader.ToString().Substring(6).Trim()));
                var loginData = authBasicToken.Split(":");

                var loginSuccess = _authenticationService.Login(loginData[0], loginData[1]);
                if (loginSuccess)
                {
                    string token = _authenticationService.GenerateToken(loginData[0]);
                    return Ok(token);
                }
                return Unauthorized("email or password is wrong.");
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError); }

        }
    }
}