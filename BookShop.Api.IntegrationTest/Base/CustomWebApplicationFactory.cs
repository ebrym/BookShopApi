
using BookShop.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace BookShop.API.IntegrationTests.Base
{
    public class CustomWebApplicationFactory<TStartup>
            : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer("ApplicationDbContext");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ApplicationDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();


                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test messages. Error: {ex.Message}");
                    }
                };
            });
        }

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

        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}
