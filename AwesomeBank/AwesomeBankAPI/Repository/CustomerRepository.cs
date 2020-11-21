using AwesomeBankAPI.Models;
using AwesomeBankAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Repository
{
    public class CustomerRepository : SqlGenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AwesomeBankDbContext context) : base(context) { }
        public Customer GetByEmail(string email)
        {
            return _context.Customers.FirstOrDefault(x => x.Email == email);
        }
    }
}
