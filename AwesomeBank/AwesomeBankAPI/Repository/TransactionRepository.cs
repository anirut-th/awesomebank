using AwesomeBankAPI.Models;
using AwesomeBankAPI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Repository
{
    public class TransactionRepository : SqlGenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AwesomeBankDbContext context) : base(context) { }
    }
}
