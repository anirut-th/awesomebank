using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.DTOs
{
    public class AccountReadDto
    {
        public string Iban { get; set; }
        public string Name { get; set; }
        public decimal BalanceAmount { get; set; }

        public static explicit operator AccountReadDto(Account account)
        {
            return new AccountReadDto
            {
                Iban = account.Iban,
                Name = account.Name,
                BalanceAmount = account.BalanceAmount
            };
        }
    }

    public class AccountWriteDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public decimal BalanceAmount { get; set; }

        public static explicit operator Account(AccountWriteDto account)
        {
            return new Account
            {
                CustomerId = account.CustomerId,
                Name = account.Name,
                BalanceAmount = account.BalanceAmount
            };
        }
    }

    public class MakeTransferData
    {
        public string senderIban { get; set; }
        public string receiverIban { get; set; }
        public decimal amount { get; set; }
    }

    public class MakeDepositData
    {
        public string accountIban { get; set; }
        public decimal amount { get; set; }
    }
}
