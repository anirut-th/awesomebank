using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.DTOs
{
    public class CustomerProfileDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerRegisterData
    { 
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
