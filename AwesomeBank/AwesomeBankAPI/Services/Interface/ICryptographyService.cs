using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface ICryptographyService
    {
        public string ComputeHash(string text); 
    }
}
