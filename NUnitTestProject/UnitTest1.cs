using BookShop.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitIntegrationTests;
using XUnitIntegrationTests.AuthenticationHelper;
using XUnitIntegrationTests.Models;
using XUnitIntegrationTests.WebFactory;

namespace NUnitTestProject
{
    public class Tests : IClassFixture<WebFactory<IntegrationTestStartup>>, IDisposable
    {
        private readonly WebFactory<IntegrationTestStartup> factory;
        private readonly HttpClient client;
        private readonly AuthHelper auth;
        private readonly string testConnectionString;

        public Tests(WebFactory<IntegrationTestStartup> factory)
        {
            this.factory = factory;
            this.client = factory.CreateClient();
            this.auth = new AuthHelper(this.client);

            var scopeFactory = this.factory.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                ////test connection string
                //var testGuid = Guid.NewGuid().ToString();
                //this.testConnectionString = "Server=.\\sqlexpress;Database=AssetManagementTestDb_" + testGuid.ToString() + ";Trusted_Connection=True;MultipleActiveResultSets=true";

                ////set new test connection string
                //var conn = dbContext.Database.GetDbConnection().ConnectionString;
                //dbContext.Database.GetDbConnection().ConnectionString = this.testConnectionString;

                // Initialize database
                //  Ensure the database is deleted before each test.
                dbContext.Database.EnsureDeleted();
                // Ensure the database is created.
                // dbContext.Database.EnsureCreated();

                //Ensure migration
                dbContext.Database.Migrate();
            }
        }
        //[SetUp]
        //public void Setup()
        //{
        //}

        //[Test]
        //public void Test1()
        //{
        //    Assert.Pass();
        //}
        [Fact]
        public async Task Get_Book()
        {
            //Arrange
            LoginToken loginToken = await auth.GetTokenForTestUser(null, factory);


            //  var stock = await StockSeedData.CreateStockForTest(factory, this.testConnectionString);
            string url = "/api/v1.0/Book/get";
            //Act
            

            var getResponse = await auth.SendRequest(HttpMethod.Get, url, null, loginToken);
            var getResponseString = await getResponse.Content.ReadAsStringAsync();

            //Assert
            Xunit.Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        public void Dispose()
        {
            var scopeFactory = this.factory.Services.GetService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                //  dbContext.Database.GetDbConnection().ConnectionString = this.testConnectionString;
                // Initialize database
                //  Ensure the database is deleted before each test.
                dbContext.Database.EnsureDeleted();
                dbContext.Dispose();
            }
        }
    }
}