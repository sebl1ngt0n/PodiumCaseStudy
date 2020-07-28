using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoFixture;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using PodiumCaseStudy.Models;
using Xunit;

namespace PodiumCaseStudy.Tests.Context
{
    public class MoqBaseContext
    {
        protected TestServer _testServer;

        private readonly Fixture _fixture;

        private HttpClient httpClient;
        private HttpResponseMessage response;
        private Guid userId;
        private RegisterModel registerModel;

        public MoqBaseContext()
        {
            _fixture = new Fixture();
            _fixture.Customize<RegisterModel>(x => x
                .With(x => x.Email, _fixture.Create<MailAddress>().Address)
                .With(x => x.Firstname, _fixture.Create<string>().Substring(0, 5))
                .With(x => x.Surname, _fixture.Create<string>().Substring(0, 5)));
        }

        public void Given_http_client()
        {
            httpClient = _testServer.CreateClient();
        }

        public void Given_register_model(int age)
        {
            registerModel = _fixture.Create<RegisterModel>();
            registerModel.DateOfBirth = DateTime.Now.AddYears(-age);
        }

        public async Task When_user_registers()
        {
            var registerBodyJson = JsonConvert.SerializeObject(registerModel);
            response = await httpClient.PostAsync(Helper.RegisterUrl, new StringContent(registerBodyJson, Encoding.UTF8, "application/json"));
        }

        public async Task When_mortgage_searched(MortgageProductQuery mortgageProductQuery)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(mortgageProductQuery.UserId)] = userId.ToString();
            query[nameof(mortgageProductQuery.PropertyValue)] = mortgageProductQuery.PropertyValue.ToString();
            query[nameof(mortgageProductQuery.Deposit)] = mortgageProductQuery.Deposit.ToString();

            response = await httpClient.GetAsync($"{Helper.MortgageUrl}?{query}");
        }

        public void Then_status_code_is_returned(HttpStatusCode statusCode)
        {
            Assert.Equal(response.StatusCode, statusCode);
        }

        public async Task Then_guid_returns_in_response()
        {
            var responseString = await response.Content.ReadAsAsync<string>();
            Assert.NotNull(responseString);
            Assert.True(Guid.TryParse(responseString, out userId));
        }

        public async Task Then_total_products_returned(int productCount)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseString);

            var products = JsonConvert.DeserializeObject<List<MortgageProduct>>(responseString);

            Assert.NotNull(products);
            Assert.Equal(productCount, products.Count);
        }
    }
}
