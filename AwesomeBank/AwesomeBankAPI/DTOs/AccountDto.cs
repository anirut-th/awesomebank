using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.DTOs
{
    public class AccountReadDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Iban { get; set; }
        public string Name { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class AccountWriteDto
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public decimal BalanceAmount { get; set; }
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
