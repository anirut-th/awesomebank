using AutoMapper;
using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.MapProfile
{
    public class AutoMapProfile : Profile
    {
        public AutoMapProfile()
        {
            CreateMap<Customer, CustomerProfileDto>();
            CreateMap<CustomerWriteDto, Customer>();

            CreateMap<Account, AccountReadDto>();
            CreateMap<AccountWriteDto, Account>();
        }
    }
}
