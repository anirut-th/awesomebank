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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, 
            IMapper mapper, 
            ICustomerService customerService,
            ITransactionService transactionService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _customerService = customerService;
            _transactionService = transactionService;
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

        [HttpPost]
        public ActionResult CreateAccount(string fullName, string email, decimal initialAmount)
        {
            try
            {
                var customer = _customerService.GetCustomerByEmail(email);

                //If customer not exist (New customer) => create new customer
                if (customer == null)
                {
                    var _customerDto = new CustomerWriteDto
                    {
                        Email = email,
                        FullName = fullName
                    };
                    customer = _customerService.CreateCustomer(_mapper.Map<Customer>(_customerDto));
                    if (customer == null) { throw new Exception("Unable to create customer."); }
                }

                var accountDto = new AccountWriteDto
                {
                    Name = customer.FullName,
                    CustomerId = customer.Id,
                    BalanceAmount = 0 //initial with 0, the initialAmount will apply after create a transaction. 
                };
                var account = _accountService.CreateAccount(_mapper.Map<Account>(accountDto));
                if (account == null)
                {
                    throw new Exception("Unable to create account");
                }

                //Create transaction for initial amount
                Transaction transaction = _transactionService.MakeDeposit(account.Id, initialAmount);
                if (transaction == null)
                { 
                    throw new Exception("Unable to create transaction");
                }

                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("deposit")]
        public ActionResult MakeDeposit(string accountId, decimal amount)
        {
            try
            {
                Guid Id = Guid.Parse(accountId);

                //Create deposit transaction
                Transaction transaction = _transactionService.MakeDeposit(Id, amount);
                if (transaction == null)
                {
                    throw new Exception("Unable to create transaction");
                }

                return Ok("Success");
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError); }
        }

        [HttpPost]
        [Route("transfer")]
        public ActionResult MakeTransfer(string senderAccountId, string receiverAccountId, decimal amount)
        {
            try
            {
                Guid _senderAccountId = Guid.Parse(senderAccountId);
                Guid _receiverAccountId = Guid.Parse(receiverAccountId);
                
                //Create deposit transaction
                Transaction transaction = _transactionService.MakeTransfer(_senderAccountId, _receiverAccountId, amount);
                if (transaction == null)
                {
                    throw new Exception("Unable to create transaction");
                }

                return Ok("Success");
            }
            catch { return StatusCode((int)HttpStatusCode.InternalServerError); }
        }
    }
}