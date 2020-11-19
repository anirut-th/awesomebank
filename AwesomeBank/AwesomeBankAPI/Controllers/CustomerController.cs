using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeBankAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerReadDto> GetCustomer(string id)
        {
            try
            {
                Guid _Id = Guid.Parse(id);
                var result = _customerService.GetCustomer(_Id);
                if (result != null)
                {
                    return Ok(_mapper.Map<CustomerReadDto>(result));
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
        public ActionResult CustomerRegister(string fullname, string email, string password)
        {
            try
            {
                bool isValid = _customerService.ValidateRegisterData(fullname, email);
                if (!isValid) { return BadRequest(); }

                var customerModel = new Customer
                {
                    Email = email,
                    FullName = fullname,
                    Password = password
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