using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AwesomeBankAPI.Test
{
    public class CustomerControllerTest : IntegrationTest
    {
        [Fact]
        public void GetProfile_WithAuth_ReturnOkResponse()
        {
            //Arrange
            GetAuthenticate();

            //Act
            var response = testClient.GetAsync("https://localhost:44367/api/customer/profile").Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
