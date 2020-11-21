using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AwesomeBankAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AwesomeBankAPI.Test
{
    public class IntegrationTest
    {
        protected readonly HttpClient testClient;
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(AwesomeBankDbContext));
                        services.AddDbContext<AwesomeBankDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            testClient = appFactory.CreateClient();
        }

        protected async Task GetAuthenticateAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetTokenAsync());
        }

        private async Task<string> GetTokenAsync()
        {
            throw new NotImplementedException();
        }
    }
}
