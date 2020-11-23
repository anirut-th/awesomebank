using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
