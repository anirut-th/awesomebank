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
using AwesomeBankAPI.Models;

namespace AwesomeBankAPI.Test
{
    public class IntegrationTest
    {
        protected readonly HttpClient testClient;
        protected Account testAccount1;
        protected Account testAccount2;
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

            //Generate Test Customer
            var postContent = new StringContent(JsonConvert.SerializeObject(new CustomerRegisterData
            {
                fullname = "test",
                email = "test@email.com",
                password = "password123"
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = testClient.PostAsync("http://localhost/api/customer/register", postContent).Result;

            //Generate Test Account
            postContent = new StringContent(JsonConvert.SerializeObject("0"), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;
            testAccount1 = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().Result);

            postContent = new StringContent(JsonConvert.SerializeObject(1000000000), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;
            testAccount2 = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().Result);
        }

        protected void GetAuthenticate()
        {
            testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
        }
        protected void GetAnonymous()
        {
            testClient.DefaultRequestHeaders.Authorization = null;
        }

        private string GetToken()
        {
            testClient.DefaultRequestHeaders.Add($"Authorization", $"Basic {Base64Encode("test@email.com:password123")}");
            var token = testClient.PostAsync("http://localhost/api/auth/token", null).Result.Content.ReadAsStringAsync().Result;
            token = token.Replace("\"", String.Empty);
            return token;
        }

        private string Base64Encode(string textToEncode)
        {
            byte[] textAsBytes = Encoding.UTF8.GetBytes(textToEncode);
            return Convert.ToBase64String(textAsBytes);
        }
    }
}
