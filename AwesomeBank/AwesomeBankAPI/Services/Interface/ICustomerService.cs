using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface ICustomerService
    {
        Customer CreateCustomer(Customer customer);
        Customer GetCustomer(Guid Id);
        bool ValidateRegisterData(string email, string fullName);
    }
}
