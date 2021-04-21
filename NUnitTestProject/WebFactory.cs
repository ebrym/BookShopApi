
using BookShop.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace XUnitIntegrationTests.WebFactory
{
    public class WebFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            return WebHost.CreateDefaultBuilder(null)
                .UseSolutionRelativeContentRoot("BookShop")
                .ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                })
                .ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                })
                .UseWebRoot("wwwroot")
                .UseStartup<TEntryPoint>();
        }

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureServices(services =>
        //    {
        //        //// Create a new service provider.
        //        //var serviceProvider = new ServiceCollection()
        //        //    .AddEntityFrameworkInMemoryDatabase()
        //        //    .BuildServiceProvider();
        //        ////remove use sql defined in live code
        //        //var descriptorDbContext = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(ApplicationDbContext));
        //        //bool removed = services.Remove(descriptorDbContext);
        //        //// Add a database context (ApplicationDbContext) using an in-memory database for testing.
        //        //services.AddDbContext<ApplicationDbContext>(options =>
        //        //{
        //        //    options.UseInMemoryDatabase("InMemoryAppDb");
        //        //    //creates db with different guid names per test. This prevent using the ensuredelete call below
        //        //    // options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        //        //    options.UseInternalServiceProvider(serviceProvider);
        //        //});

        //        //// Build the service provider.
        //        //var sp = services.BuildServiceProvider();

        //        //// Create a scope to obtain a reference to the database contexts
        //        //using (var scope = sp.CreateScope())
        //        //{
        //        //    var scopedServices = scope.ServiceProvider;

        //        //    var appDb = scopedServices.GetRequiredService<ApplicationDbContext>();
        //        //    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<IntegrationTestStartup>>>();

        //        //  //  Ensure the database is deleted before each test.
        //        //      appDb.Database.EnsureDeleted();
        //        //   // Ensure the database is created.
        //        //   appDb.Database.EnsureCreated();
        //        //}
        //    });
        //}
    }
}