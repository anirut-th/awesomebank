using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccoundId { get; set; }
        public string TransactionType { get; set; }

        public Guid? ReceiverAccoundId { get; set; }
        public decimal Amount { get; set; }
        public Guid ActionByCustomerId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
