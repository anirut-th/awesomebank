using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository.Interface;

namespace AwesomeBankAPI.Repository
{
    public class MockTransactionRepository : ITransactionRepository
    {
        public int Add(Transaction model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public Transaction GetById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetMultiple(Expression<Func<Transaction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Transaction GetSingle(Expression<Func<Transaction, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Transaction model)
        {
            throw new NotImplementedException();
        }
    }
}
