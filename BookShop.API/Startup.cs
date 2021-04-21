using BookShop.Api;
using BookShop.Infrastructure;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllers().AddFluentValidation(x =>
            {
                x.AutomaticValidationEnabled = false;
            });
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddApiVersion();
            services.AddDocSwagger();
            services.AddSerilogLogger(Configuration);
            services.AddIdentity();
            services.AddCors();
            services.AddResponseCaching();
            services.AddHttpClient("HttpClient").AddPolicyHandler(x =>
            {
                return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(3, retry)));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger, ApplicationDbContext context)
        {
            if (env.IsProduction())
            {
                try
                {
                    var ctx = app.ApplicationServices.GetService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (System.Exception exception)
                {
                    logger.Error(exception.Message);
                }
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }

            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });

            app.UseDocSwagger();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            //app.ConfigureExceptionHandler(logger);
            app.UseResponseCaching();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
