using AwesomeBankAPI.Config;
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

        public Account CreateAccount(Account account)
        {
            try
            {
                account.Id = Guid.NewGuid();
                account.CreatedDate = DateTime.Now;
                account.IsActive = true;

                var result = _accountRepository.Add(account);
                if (result == (int)GlobalConfig.Result.SUCCESS)
                {
                    return account;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public string GenerateIBAN()
        {
            throw new NotImplementedException();
        }

        public Account GetAccount(Guid Id)
        {
            return _accountRepository.GetById(Id);
        }

        public bool ValidateIBAN(string iban)
        {
            throw new NotImplementedException();
        }
    }
}
