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

        public const decimal DEPOSIT_FEE = 0.1M;
        public const string TOKEN_ISSUER = "awesomebank";
        public const string TOKEN_AUDIENCE = "awesomebank";

        public const string MAIN_COUNTRY_CODE = "NL";
        public const string BANK_CODE = "0010";
        public const string ACCOUNT_PREFIX = "000019";
    }
}
