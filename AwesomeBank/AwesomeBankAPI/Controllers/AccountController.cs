using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        private readonly IAuthenticationService _authenticationService;
        public AccountController(IAccountService accountService, 
            ICustomerService customerService,
            ITransactionService transactionService,
            IAuthenticationService authenticationService) : base(customerService)
        {
           
            _accountService = accountService;
            _customerService = customerService;
            _transactionService = transactionService;
            _authenticationService = authenticationService;
        }

        [HttpGet("{id}")]
        public ActionResult GetAccount(string iban)
        {
            var account = _accountService.GetAccount(iban);
            if (account == null)
            {
                return NotFound();
            }
            var IsAuthorize = _authenticationService.CheckAccountAuthorize(base.CustomerData.Id, account.Id);
            if (!IsAuthorize)
            {
                return Unauthorized();
            }
            return Ok(account);
        }

        [HttpPost]
        public ActionResult CreateAccount([FromBody]decimal initialAmount)
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
                var account = _accountService.CreateAccount((Account)accountDto);
                if (account == null)
                {
                    throw new Exception("Unable to create account");
                }

                //Create transaction for initial amount
                Transaction transaction = _transactionService.MakeDeposit(account.Id, initialAmount, customer.Id, false);
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
        public ActionResult MakeDeposit([FromBody]MakeDepositData data)
        {
            try
            {
                var customer = _customerService.GetCustomerByEmail(base.CustomerIdentityEmail);
                var account = _accountService.GetAccount(data.accountIban);

                //Create deposit transaction
                Transaction transaction = _transactionService.MakeDeposit(account.Id, data.amount, customer.Id, true);
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
        public ActionResult MakeTransfer([FromBody]MakeTransferData data)
        {
            try
            {
                var customer = base.CustomerData;
                
                //Create deposit transaction
                Transaction transaction = _transactionService.MakeTransfer(data.senderIban, data.receiverIban, data.amount, customer.Id, true);
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