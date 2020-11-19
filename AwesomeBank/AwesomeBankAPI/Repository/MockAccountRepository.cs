using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository.Interface;

namespace AwesomeBankAPI.Repository
{
    public class MockAccountRepository : IAccountRepository
    {
        public int Add(Account model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public Account GetById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetMultiple(Expression<Func<Account, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Account GetSingle()
        {
            throw new NotImplementedException();
        }

        public Account GetSingle(Expression<Func<Account, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Account model)
        {
            throw new NotImplementedException();
        }
    }
}
