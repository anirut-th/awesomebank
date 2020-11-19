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
        private List<Customer> mockData = new List<Customer>
        {
            new Customer
            {
                Id = Guid.Parse("3682C11D-A560-4B49-AF1F-A631F03A77E7"),
                Email = "John@email.com",
                FullName = "John Doe",
                IsActive = true,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            },
            new Customer
            {
                Id = Guid.Parse("8A8E9021-4F8A-46AE-9042-4F4B7AE3B261"),
                Email = "Rami@email.com",
                FullName = "Rami Horn",
                IsActive = true,
                CreatedBy = "System",
                CreatedDate = DateTime.Now
            }
        };

        public IEnumerable<Customer> GetAll()
        {
            return mockData;
        }

        public Customer GetById(Guid Id)
        {
            return mockData.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Customer> GetMultiple(Expression<Func<Customer, bool>> predicate)
        {
            return mockData.AsQueryable<Customer>().Where(predicate).ToList();
        }

        public Customer GetSingle(Expression<Func<Customer, bool>> predicate)
        {
            return mockData.AsQueryable<Customer>().FirstOrDefault(predicate);
        }
        public Customer GetByEmail(string email)
        {
            return mockData.FirstOrDefault(x => x.Email == email);
        }

        public int Add(Customer model)
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
