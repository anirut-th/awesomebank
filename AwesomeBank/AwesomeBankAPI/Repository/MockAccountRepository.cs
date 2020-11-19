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
    public class MockAccountRepository : IAccountRepository
    {
        private List<Account> mockData = new List<Account>
        {
            new Account
            {
                Id = Guid.Parse("29870AD7-31CF-43EA-A333-57B67998E950"),
                BalanceAmount = 1000,
                Iban = "NL25INGB8011260901",
                CustomerId = Guid.Parse("AE716554-6B93-434E-96F6-8E9229C6644D"),
                Name = "John Doe",
                IsActive = true,
                CreatedDate = DateTime.Now
            },

            new Account
            {
                Id = Guid.Parse("B7341D93-8579-4C05-9E55-CAACB4A5C0D4"),
                BalanceAmount = 1500,
                Iban = "NL49INGB8589312569",
                CustomerId = Guid.Parse("6C3E8C57-33AB-4553-A0F3-DDAD208ABF25"),
                Name = "Rami Horn",
                IsActive = true,
                CreatedDate = DateTime.Now
            }
        };
        public IEnumerable<Account> GetAll()
        {
            return mockData;
        }

        public Account GetById(Guid Id)
        {
            return mockData.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Account> GetMultiple(Expression<Func<Account, bool>> predicate)
        {
            return mockData.AsQueryable<Account>().Where(predicate).ToList();
        }

        public Account GetSingle(Expression<Func<Account, bool>> predicate)
        {
            return mockData.AsQueryable<Account>().FirstOrDefault(predicate);
        }

        public Account GetByIban(string Iban)
        {
            return mockData.AsQueryable<Account>().FirstOrDefault(x => x.Iban == Iban);
        }

        public int Add(Account model)
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
