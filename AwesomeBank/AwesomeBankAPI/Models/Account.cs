using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Models
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        [MaxLength(25)]
        public string Iban { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public decimal BalanceAmount { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
