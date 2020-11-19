using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Config
{
    public static class GlobalConfig
    {
        public enum TransactionType : int
        { 
            DEPOSIT = 100,
            TRANSFER = 200
        };

        public enum Result : int
        { 
            SUCCESS = 1,
            ERROR = 0
        }
    }
}
