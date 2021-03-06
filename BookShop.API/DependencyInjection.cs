using System;
using System.Linq;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using BookShop.Data;
using BookShop.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using OpenIddict.Abstractions;
using OpenIddict.Validation;
using Serilog;

namespace BookShop.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the identity.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, Role>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;

            });

            services.Configure<OpenIdConnectServerOptions>(opt =>
            {
                opt.AllowInsecureHttp = true;
            });
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddictValidationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationDefaults.AuthenticationScheme;
            });

            services.AddOpenIddict()
                .AddCore(options =>
                {

                    options.UseEntityFrameworkCore()
                        .UseDbContext<ApplicationDbContext>();


                })

                .AddServer(options =>
                {
                    options.UseMvc();


                    options.EnableAuthorizationEndpoint("/connect/authorize")
                        .EnableIntrospectionEndpoint("/connect/introspect")
                        .EnableTokenEndpoint("/connect/token");

                    options.AllowPasswordFlow();
                    options.AllowAuthorizationCodeFlow();
                    options.AllowImplicitFlow();
                    options.AllowClientCredentialsFlow();
                    options.DisableHttpsRequirement();
                    options.AcceptAnonymousClients();
                    options.RegisterScopes(
                        OpenIdConnectConstants.Scopes.OpenId,
                        OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Phone,
                        OpenIdConnectConstants.Scopes.Profile,
                        OpenIdConnectConstants.Scopes.OfflineAccess,
                        OpenIddictConstants.Scopes.Roles);
                    //   if (!hostEnvironment.IsDevelopment())
                    //    options.AddSigningCertificate(new X509Certificate2(file, password));
                    //    else
                    options.AddDevelopmentSigningCertificate();
                }).AddValidation();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public static void AddApiVersion(this IServiceCollection service)
        {
            service.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public static void AddDocSwagger(this IServiceCollection service)
        {
            service.AddSwaggerDocument(x =>
            {


                x.GenerateXmlObjects = true;
                x.GenerateEnumMappingDescription = true;
                x.DocumentName = "Book Shop Apis";
                x.Title = "Book Shop";
                x.Description = "Book Shop Apis";
                x.AddSecurity("oauth2", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the text-box: Bearer {your JWT token}.",
                    Scheme = "Bearer"

                });
                x.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("oauth2"));

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseDocSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3(s =>
            {
                s.Path = "";
                s.DocumentTitle = "Asset Management Api";


            });
            app.UseReDoc(d =>
            {
                d.Path = "/redoc";

            });
        }
        /// <summary>
        /// Adds the serilog logger.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The configuration.</param>
        public static void AddSerilogLogger(this IServiceCollection services, IConfiguration config)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config).Enrich.FromLogContext().CreateLogger();

            services.AddSingleton(Log.Logger);


        }
    }
}