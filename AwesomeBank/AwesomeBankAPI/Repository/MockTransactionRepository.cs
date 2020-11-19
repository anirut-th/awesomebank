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
    public class MockTransactionRepository : ITransactionRepository
    {
        private List<Transaction> mockData = new List<Transaction>
        {
            new Transaction
            {
                Id = Guid.Parse("F73F2390-FEDA-42E4-A8B4-2FA63FF0A60B"),
                AccoundId = Guid.Parse("74DC208E-BE4D-4FDB-A0F0-29C6DFEC753E"),
                ActionByCustomerId = Guid.Parse("509F56F5-285D-48A2-B20A-30D033C05254"),
                Amount = 200,
                Description = "Deposit to Account:09382349123, Amount 200 Baht.",
                ReceiverAccoundId = null,
                TransactionType = GlobalConfig.TransactionType.DEPOSIT,
                IsActive = true,
                CreatedDate = DateTime.Now
            },

            new Transaction
            {
                Id = Guid.Parse("CB5FB9FE-8896-4F21-BEFE-CE6E4CFAF18A"),
                AccoundId = Guid.Parse("535044F5-C817-4E16-AF21-6D645383FBFB"),
                ActionByCustomerId = Guid.Parse("2691F9B1-251A-47B6-8BA0-69AFD3283C9F"),
                Amount = 500,
                Description = "Transfer from Account:09382349123 to Account:09382349123, Amount 500 Baht.",
                ReceiverAccoundId = Guid.Parse("DD3DDE66-38CC-487A-8A6C-82E5928A7D69"),
                TransactionType = GlobalConfig.TransactionType.TRANSFER,
                IsActive = true,
                CreatedDate = DateTime.Now
            },
        };
        public IEnumerable<Transaction> GetAll()
        {
            return mockData;
        }

        public Transaction GetById(Guid Id)
        {
            return mockData.FirstOrDefault(x => x.Id == Id);
        }

        public IEnumerable<Transaction> GetMultiple(Expression<Func<Transaction, bool>> predicate)
        {
            return mockData.AsQueryable<Transaction>().Where(predicate).ToList();
        }

        public Transaction GetSingle(Expression<Func<Transaction, bool>> predicate)
        {
            return mockData.AsQueryable<Transaction>().FirstOrDefault(predicate);
        }

        public int Add(Transaction model)
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
