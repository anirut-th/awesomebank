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
    public class MockCustomerRepository : ICustomerRepository
    {
        private List<Customer> mockData = new List<Customer>
        {
            new Customer
            {
                Id = Guid.Parse("3682C11D-A560-4B49-AF1F-A631F03A77E7"),
                Email = "John@email.com",
                FullName = "John Doe",
                Password = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8", //password
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new Customer
            {
                Id = Guid.Parse("8A8E9021-4F8A-46AE-9042-4F4B7AE3B261"),
                Email = "Rami@email.com",
                FullName = "Rami Horn",
                Password = "6cf615d5bcaac778352a8f1f3360d23f02f34ec182e259897fd6ce485d7870d4", //password2
                IsActive = true,
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
