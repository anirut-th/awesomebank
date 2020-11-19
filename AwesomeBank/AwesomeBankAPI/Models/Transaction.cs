using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AwesomeBankAPI.Config.GlobalConfig;

namespace AwesomeBankAPI.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccoundId { get; set; }
        public int TransactionType { get; set; }
        public Guid? ReceiverAccoundId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionFee { get; set; }
        public decimal AmountAfterFee { get; set; }
        public Guid ActionByCustomerId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
