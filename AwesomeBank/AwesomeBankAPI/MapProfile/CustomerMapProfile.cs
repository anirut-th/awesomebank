using AutoMapper;
using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.MapProfile
{
    public class CustomerMapProfile : Profile
    {
        public CustomerMapProfile()
        {
            CreateMap<Customer, CustomerReadDto>();
            CreateMap<CustomerWriteDto, Customer>();
        }
    }
}
