using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper) : base(customerService)
        {
            _customerService = customerService;
            _mapper = mapper;
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
                    return Ok(_mapper.Map<CustomerProfileDto>(result));
                }
                return NotFound();
            }
            catch 
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
                bool isValid = _customerService.ValidateRegisterData(data.fullname, data.email);
                if (!isValid) { return BadRequest(); }

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