using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService) : base(customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize]
        [Route("profile")]
        public ActionResult<CustomerProfileDto> GetCustomer()
        {
            try
            {
                var result = base.CustomerData;
                if (result != null)
                {
                    return Ok((CustomerProfileDto)(result));
                }
                return NotFound();
            }
            catch (Exception ex)
            { 
                return StatusCode((int)HttpStatusCode.InternalServerError); 
            }
        }

        [HttpPost]
        [Route("register")]
        public ActionResult CustomerRegister([FromBody]CustomerRegisterData data)
        {
            try
            {
                bool isValid = _customerService.ValidateRegisterData(data.email, data.fullname);
                if (!isValid) { return BadRequest("test test"); }

                var customerModel = new Customer
                {
                    Email = data.email,
                    FullName = data.fullname,
                    Password = data.password
                };

                var result = _customerService.CreateCustomer(customerModel);
                if (result != null)
                {
                    return Ok("Success");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError); }

        }
    }
}