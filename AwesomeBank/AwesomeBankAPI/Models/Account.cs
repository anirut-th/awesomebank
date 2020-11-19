using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Iban { get; set; }
        public string Name { get; set; }
        public decimal BalanceAmount { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
