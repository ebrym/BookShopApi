
using BookShop.API;
using BookShop.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BookShop.Api.IntegrationTest
{
    public class IntegrationTestStartup : Startup
    {
        public IntegrationTestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger loggerFactory, ApplicationDbContext context)
        {
            base.Configure(app, env, loggerFactory, context);

            var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                // StockSeedData.CreateStock(null, dbContext).Wait();
                var currentConnectionString = dbContext.Database.GetDbConnection().ConnectionString;
                
                ////test connection string
                //var testGuid = Guid.NewGuid().ToString();
                //var testConnectionString = "Server=.\\sqlexpress;Database=AssetManagementTestDb_"+ testGuid.ToString() + ";Trusted_Connection=True;MultipleActiveResultSets=true";

                ////set new test connection string
                //dbContext.Database.GetDbConnection().ConnectionString = testConnectionString;

                //Initialize database
                //  Ensure the database is deleted before each test.
                 dbContext.Database.EnsureDeleted();
                //Ensure the database is created.
                dbContext.Database.EnsureCreated();

               // Ensure migration
                  dbContext.Database.Migrate();
            }
        }
    }
}