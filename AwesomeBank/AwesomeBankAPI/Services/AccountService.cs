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
        public Account GetAccount(string Iban)
        {
            return _accountRepository.GetByIban(Iban);
        }

        public bool ApplyToBalance(Account account, decimal amount)
        {
            try
            {
                var finalAmount = account.BalanceAmount + amount;
                if (finalAmount < 0)
                {
                    return false;
                }
                account.BalanceAmount = finalAmount;
                account.UpdatedDate = DateTime.Now;
                var result = _accountRepository.Update(account);
                if (result == (int)GlobalConfig.Result.SUCCESS)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool ValidateIBAN(string iban)
        {
            throw new NotImplementedException();
        }
    }
}
