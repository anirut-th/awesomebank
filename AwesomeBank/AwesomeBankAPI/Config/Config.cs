using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Config
{
    public static class GolbalConfig
    {
        public enum TransactionType : int
        { 
            DEPOSIT = 100,
            TRANSFER = 200
        };
    }
}
