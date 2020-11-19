using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface IAccountService
    {
        Account GetAccount(Guid Id);
        Account CreateAccount(Account account);
        bool ValidateIBAN(string iban);
        string GenerateIBAN();
        bool ApplyToBalance(Account account, decimal amount);
    }
}
