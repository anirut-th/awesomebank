using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Route("api/account")]
    [Authorize]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, 
            IMapper mapper, 
            ICustomerService customerService,
            ITransactionService transactionService) : base(customerService)
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
        public ActionResult CreateAccount(decimal initialAmount)
        {
            try
            {
                var customer = base.CustomerData;
                if (customer == null)
                {
                    throw new Exception("Customer not found.");
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
                Transaction transaction = _transactionService.MakeDeposit(account.Id, initialAmount, customer.Id);
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
        public ActionResult MakeDeposit(string accountIban, decimal amount)
        {
            try
            {
                var customer = _customerService.GetCustomerByEmail(base.CustomerIdentityEmail);
                var account = _accountService.GetAccount(accountIban);

                //Create deposit transaction
                Transaction transaction = _transactionService.MakeDeposit(account.Id, amount, customer.Id);
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
                var customer = _customerService.GetCustomerByEmail(base.CustomerIdentityEmail);
                Guid _senderAccountId = Guid.Parse(senderAccountId);
                Guid _receiverAccountId = Guid.Parse(receiverAccountId);
                
                //Create deposit transaction
                Transaction transaction = _transactionService.MakeTransfer(_senderAccountId, _receiverAccountId, amount, customer.Id);
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