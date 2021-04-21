
using BookShop.Api.IntegrationTest;
using BookShop.Api.IntegrationTest.AuthenticationHelper;
using BookShop.Api.IntegrationTest.Models;
using BookShop.API.IntegrationTests.Base;
using BookShop.Data;
using BookShop.Repository.Request.User;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.API.IntegrationTests.Controllers
{

    public class CategoryControllerTests : IClassFixture<CustomWebApplicationFactory<IntegrationTestStartup>>
    {
        private readonly CustomWebApplicationFactory<IntegrationTestStartup> factory;
        //private readonly CustomWebApplicationFactory<IntegrationTestStartup> factory;
        private readonly HttpClient client;
        private readonly AuthHelper auth;
        public CategoryControllerTests(CustomWebApplicationFactory<IntegrationTestStartup> factory)
        {
        //    _factory = factory;
        //    this.client = factory.CreateClient();
        //    this.auth = new AuthHelper(this.client);


            this.factory = factory;
            this.client = factory.CreateClient();
            this.auth = new AuthHelper(this.client);

        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            var client = factory.GetAnonymousClient();

            CreateUserRequest user = new CreateUserRequest
            {
                Email = "fakeapprover@test.com",
                FullName = "Fake Approver Account",
                Password = "Welcome123@",
                Roles = new string[] { "Fake Role", },
                UserName = "fakeapprover@test.com"
            };
            LoginToken loginToken = await auth.GetTokenForTestUser(user,factory, client);

            //var response = await client.GetAsync("/api/v1/category/get");

            var url = "/api/v1/category/get";

            var response = await auth.SendRequest(HttpMethod.Post, url, null, loginToken);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<Category>>(responseString);
            
            Assert.IsType<List<Category>>(result);
            Assert.NotEmpty(result);
        }
    }
}
