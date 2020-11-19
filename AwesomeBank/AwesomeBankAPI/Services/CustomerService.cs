using AwesomeBankAPI.Config;
using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer CreateCustomer(Customer customer)
        {
            try
            {
                customer.Id = Guid.NewGuid();
                customer.IsActive = true;
                customer.CreatedDate = DateTime.Now;

                var result = _customerRepository.Add(customer);
                if (result == (int)GlobalConfig.Result.SUCCESS)
                {
                    return customer;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public Customer GetCustomerByEmail(string email)
        {
            return _customerRepository.GetByEmail(email);
        }

        public Customer GetCustomer(Guid Id)
        {
            return _customerRepository.GetById(Id);
        }

        public bool ValidateRegisterData(string email, string fullName)
        {
            try
            {
                var isDuplicateEmail = _customerRepository.GetByEmail(email) != null;
                if (isDuplicateEmail) { return false; }

                var isDuplicateFullname = _customerRepository.GetSingle(x => x.FullName == fullName) != null;
                if (isDuplicateFullname) { return false; }

                return true;
            }
            catch(Exception ex) { throw ex; }
        }
    }
}
