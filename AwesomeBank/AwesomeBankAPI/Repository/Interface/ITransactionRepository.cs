using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBankAPI.Models;

namespace AwesomeBankAPI.Repository.Interface
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
    }
}
