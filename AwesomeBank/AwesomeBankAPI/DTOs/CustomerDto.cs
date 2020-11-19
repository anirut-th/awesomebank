using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.DTOs
{
    public class CustomerReadDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CustomerWriteDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
