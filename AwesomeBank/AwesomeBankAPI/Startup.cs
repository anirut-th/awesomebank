using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeBankAPI.Repository;
using AwesomeBankAPI.Repository.Interface;
using AwesomeBankAPI.Services;
using AwesomeBankAPI.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AwesomeBankAPI.Config;
using Microsoft.EntityFrameworkCore;

namespace AwesomeBankAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICryptographyService, CryptographyService>();

            //services.AddScoped<IAccountRepository, MockAccountRepository>();
            //services.AddScoped<ICustomerRepository, MockCustomerRepository>();
            //services.AddScoped<ITransactionRepository, MockTransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            X509Certificate2 cert = new X509Certificate2(Configuration["Certificate:PublicCertificate"]);
            X509SecurityKey key = new X509SecurityKey(cert);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = GlobalConfig.TOKEN_AUDIENCE,
                    ValidIssuer = GlobalConfig.TOKEN_ISSUER,
                    IssuerSigningKey = key
                };
            });

            services.AddDbContext<AwesomeBankDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AwesomeDatabase")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
