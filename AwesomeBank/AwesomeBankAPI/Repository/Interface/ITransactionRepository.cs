using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBankAPI.Models;

namespace AwesomeBankAPI.Repository.Interface
{
    interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }
}
