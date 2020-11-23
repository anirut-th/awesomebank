using AwesomeBankAPI.DTOs;
using AwesomeBankAPI.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;

namespace AwesomeBankAPI.Test
{
    public class AccountControllerTest : IntegrationTest
    {
        public AccountControllerTest()
        {
            //Generate Test Account
            GetAuthenticate();
            var postContent = new StringContent(JsonConvert.SerializeObject(0), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;
            testAccount1 = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().Result);

            postContent = new StringContent(JsonConvert.SerializeObject(1000000000), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;
            testAccount2 = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().Result);
            GetAnonymous();
        }

        [Theory]
        [InlineData("https://localhost/api/account/")]
        [InlineData("https://localhost/api/account/EEF44E60-547C-4A84-9976-92A21704FD5C")]
        [InlineData("https://localhost/api/account/transfer")]
        [InlineData("https://localhost/api/account/deposit")]
        public void Endpoint_WithoutAuth_ReturnUnauthorized(string url)
        {
            //Arrange
            GetAnonymous();

            //Act
            var response = testClient.GetAsync(url).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Theory]
        [InlineData("https://localhost/api/account/")]
        [InlineData("https://localhost/api/account/EEF44E60-547C-4A84-9976-92A21704FD5C")]
        [InlineData("https://localhost/api/account/transfer")]
        [InlineData("https://localhost/api/account/deposit")]
        public void Endpoint_WithAuth_NotReturnUnauthorized(string url)
        {
            //Arrange
            GetAnonymous();

            //Act
            var response = testClient.GetAsync(url).Result;

            //Assert
            response.StatusCode.Should().NotBe(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public void GetAccount_WithParameter_ReturnOKAndSingleAccount()
        {
            //Arrange
            GetAuthenticate();

            //Act
            var response = testClient.GetAsync("https://localhost/api/account/" + testAccount1.Id).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var account = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().Result);
            account.Id.Should().Be(testAccount1.Id);
            GetAnonymous();
        }

        [Fact]
        public void GetAccount_WithoutParameter_ReturnOKAndAccountList()
        {
            //Arrange
            GetAuthenticate();

            //Act
            var response = testClient.GetAsync("https://localhost/api/account/").Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(response.Content.ReadAsStringAsync().Result);
            accounts.Should().Contain(testAccount1);
            accounts.Should().Contain(testAccount2);
        }

        [Fact]
        public void CreateAccount_SendCorrectFormat_ReturnOkResponse()
        {
            //Arrange
            GetAuthenticate();
            var postContent = new StringContent(JsonConvert.SerializeObject(2000), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void CreateAccount_SendInCorrectFormat_ReturnOkResponse()
        {
            //Arrange
            GetAuthenticate();
            var postContent = new StringContent(JsonConvert.SerializeObject("some text"), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("https://localhost/api/account/", postContent).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Theory]
        [InlineData(1000, 999)]
        [InlineData(2330, 2327.67)]
        [InlineData(23435657, 23412221.34)]
        [InlineData(434598, 434163.40)]
        [InlineData(99069, 98969.93)]
        [InlineData(6000, 5994)]
        [InlineData(63456, 63392.54)]
        [InlineData(892567, 891674.43)]
        [InlineData(24577, 24552.42)]
        [InlineData(3546, 3542.45)]
        public void MakeDeposit_ToExistingAccount_ReturnOKAndHaveCorrectBalance(decimal amount, decimal expected)
        {
            //Arrange
            GetAuthenticate();
            var postContent = new StringContent(JsonConvert.SerializeObject(new MakeDepositData
            { 
                accountIban = testAccount1.Iban,
                amount = amount
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("https://localhost/api/account/deposit", postContent).Result;
            
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getaccount = testClient.GetAsync("https://localhost/api/account/" + testAccount1.Iban).Result;
            var account = JsonConvert.DeserializeObject<Account>(getaccount.Content.ReadAsStringAsync().Result);
            account.BalanceAmount.Should().Be(expected);
        }

        [Theory]
        [InlineData(1000, 999, 999999000)]
        [InlineData(2330, 2327.67, 999997670)]
        [InlineData(23435657, 23412221.34, 976564343)]
        [InlineData(434598, 434163.40, 999565402)]
        [InlineData(99069, 98969.93, 999900931)]
        [InlineData(6000, 5994, 999994000)]
        [InlineData(63456, 63392.54, 999936544)]
        [InlineData(892567, 891674.43, 999107433)]
        [InlineData(24577, 24552.42, 999975423)]
        [InlineData(3546, 3542.45, 999996454)]
        public void MakeTransfer_ToExistingAccount_ReturnOKAndHaveCorrectBalance(decimal transferAmount, decimal expectedReceiverBalance, decimal expectedSenderBalance)
        {
            //Arrange
            GetAuthenticate();
            var postContent = new StringContent(JsonConvert.SerializeObject(new MakeTransferData
            {
                senderIban = testAccount2.Iban,
                receiverIban = testAccount1.Iban,
                amount = transferAmount
            }), Encoding.UTF8, "application/json");
            testClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Act
            var response = testClient.PostAsync("https://localhost/api/account/deposit", postContent).Result;

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var getSenderAccount = testClient.GetAsync("https://localhost/api/account/" + testAccount2.Iban).Result;
            var getReceiverAccount = testClient.GetAsync("https://localhost/api/account/" + testAccount1.Iban).Result;
            var senderAccount = JsonConvert.DeserializeObject<Account>(getSenderAccount.Content.ReadAsStringAsync().Result);
            var receiverAccount = JsonConvert.DeserializeObject<Account>(getReceiverAccount.Content.ReadAsStringAsync().Result);

            senderAccount.BalanceAmount.Should().Be(expectedReceiverBalance);
            receiverAccount.BalanceAmount.Should().Be(expectedSenderBalance);
        }
    }
}
