using AwesomeBankAPI.DTOs;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeBankAPI.Test
{
    public class CustomerControllerTest : IntegrationTest
    {
        public CustomerControllerTest()
        {
        }

        [Fact]
        public void GetProfile_WithAuth_ReturnOkResponse()
        {
            //Arrange
            GetAuthenticate();

            //Act
            var response = testClient.GetAsync("https://localhost/api/customer/profile").Result;

            //Assert
            var customer = JsonConvert.DeserializeObject<CustomerProfileDto>(response.Content.ReadAsStringAsync().Result);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            customer.Email.Should().Be("test@email.com");
            customer.FullName.Should().Be("test");
        }

        [Fact]
        public void GetProfile_WithoutAuth_ReturnUnauthorizedResponse()
        {
            //Arrange

            //Act
            var response = testClient.GetAsync("https://localhost/api/customer/profile").Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void CustomerRegister_CorrectFormat_ReturnOkResponse()
        { 
            //Arrange
            var postContent = new StringContent(JsonConvert.SerializeObject(new CustomerRegisterData
            {
                fullname = "test2",
                email = "test2@email.com",
                password = "password123"
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("http://localhost/api/customer/register", postContent).Result;
            
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void CustomerRegister_MissingSomeData_ReturnBadRequestResponse()
        {
            //Arrange
            var postContent = new StringContent(JsonConvert.SerializeObject(new CustomerRegisterData
            {
                fullname = "test",
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("http://localhost/api/customer/register", postContent).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
