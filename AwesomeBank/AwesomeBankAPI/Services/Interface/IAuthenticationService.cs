using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services.Interface
{
    public interface IAuthenticationService
    {
        string GenerateToken(string username);
        bool Login(string email, string password);

        bool CheckAccountAuthorize(Guid customerId, Guid accountId);
    }
}
