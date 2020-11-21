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
using AwesomeBankAPI.DTOs;

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

        protected void GetAuthenticate()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", GetToken());
        }

        private string GetToken()
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(new CustomerRegisterData
            {
                fullname = "test",
                email = "test@email.com",
                password = "password123"
            }));
            var response = testClient.PostAsync("https://localhost:44367/api/customer/register", postContent);
            if (response.Result.IsSuccessStatusCode)
            {
                testClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {Base64Encode("test@email.com:password123")}");
                var token = testClient.PostAsync("https://localhost:44367/api/auth/token", null).Result.Content.ReadAsStringAsync().Result;
                return token;
            }
            else
            {
                return null;
            }
        }

        private string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }
    }
}
