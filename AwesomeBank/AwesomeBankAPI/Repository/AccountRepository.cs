using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Config;

namespace AwesomeBankAPI.Repository
{
    public class AccountRepository : SqlGenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(AwesomeBankDbContext context) : base(context)
        { 
        }

        public Account GetByIban(string Iban)
        {
            return _context.Accounts.FirstOrDefault(x => x.Iban == Iban);
        }
    }
}
