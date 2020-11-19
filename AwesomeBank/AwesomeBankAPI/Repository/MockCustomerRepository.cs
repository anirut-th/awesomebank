using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository.Interface;

namespace AwesomeBankAPI.Repository
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public int Add(Customer model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetMultiple(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Customer GetSingle(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public int Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public int Update(Customer model)
        {
            throw new NotImplementedException();
        }
    }
}
