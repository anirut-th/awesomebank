using AwesomeBankAPI.Config;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AwesomeBankAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;
        public AuthenticationService(
            ICustomerRepository customerRepository, 
            ICryptographyService cryptographyService,
            IConfiguration configuration,
            IAccountRepository accountRepository)
        {
            _customerRepository = customerRepository;
            _cryptographyService = cryptographyService;
            _configuration = configuration;
            _accountRepository = accountRepository;
        }

        public bool CheckAccountAuthorize(Guid customerId, Guid accountId)
        {
            try
            {
                var account = _accountRepository.GetById(accountId);
                if (account.CustomerId == customerId)
                {
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public string GenerateToken(string email)
        {
            X509Certificate2 cert = new X509Certificate2(_configuration["Certificate:CertificateKey"], _configuration["Certificate:Password"]);
            X509SecurityKey key = new X509SecurityKey(cert);

            var claims = new[] { new Claim(ClaimTypes.Email, email) };
            var secToken = new JwtSecurityToken(
                GlobalConfig.TOKEN_ISSUER,
                GlobalConfig.TOKEN_AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature));
            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            return jwtSecurityHandler.WriteToken(secToken);
        }

        public bool Login(string email, string password)
        {
            try
            {
                var passwordHash = _cryptographyService.ComputeHash(password);
                var customer = _customerRepository.GetSingle(x => x.Email == email && x.Password == passwordHash);
                if (customer == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
    }
}
