using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static AwesomeBankAPI.Config.GlobalConfig;

namespace AwesomeBankAPI.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AccoundId { get; set; }

        [Required]
        public int TransactionType { get; set; }

        public Guid? ReceiverAccoundId { get; set; }
        
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal TransactionFee { get; set; }

        [Required]
        public decimal AmountAfterFee { get; set; }

        [Required]
        public Guid ActionByCustomerId { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
