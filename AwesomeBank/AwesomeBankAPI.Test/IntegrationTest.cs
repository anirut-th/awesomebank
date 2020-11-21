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
using System.Linq;

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
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AwesomeBankDbContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }
                        services.AddDbContext<AwesomeBankDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("AwesomeDB");
                        });
                    });
                });
            testClient = appFactory.CreateClient();
        }

        protected void GetAuthenticate()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
        }

        private string GetToken()
        {
            var postContent = new StringContent(JsonConvert.SerializeObject(new CustomerRegisterData
            {
                fullname = "test",
                email = "test@email.com",
                password = "password123"
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = testClient.PostAsync("http://localhost/api/customer/register", postContent);
            if (response.Result.IsSuccessStatusCode)
            {
                testClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {Base64Encode("test@email.com:password123")}");
                var token = testClient.PostAsync("http://localhost/api/auth/token", null).Result.Content.ReadAsStringAsync().Result;
                token = token.Replace("\"", String.Empty);
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
