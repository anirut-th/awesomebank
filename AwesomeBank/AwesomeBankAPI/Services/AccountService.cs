using AwesomeBankAPI.Models;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services
{
    public class AccountService : IAccountService
    {
        public IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository; 
        }

        public Account GetAccount(Guid Id)
        {
            return _accountRepository.GetById(Id);
        }
    }
}
