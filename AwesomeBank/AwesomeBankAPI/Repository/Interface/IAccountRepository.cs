using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Repository.Interface
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Account GetByIban(string Iban);
    }
}
