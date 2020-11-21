using AwesomeBankAPI.Models;
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

        public static explicit operator CustomerProfileDto(Customer customer)
        {
            return new CustomerProfileDto
            {
                FullName = customer.FullName,
                Email = customer.Email
            };
        }
    }

    public class CustomerRegisterData
    { 
        public string fullname { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public static explicit operator Customer(CustomerRegisterData customerRegister)
        {
            return new Customer
            {
                Email = customerRegister.email,
                FullName = customerRegister.fullname,
                Password = customerRegister.password
            };
        }
    }
}
