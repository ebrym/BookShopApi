using System.Reflection;
using AutoMapper;
using BookShop.Repository.Common.Behaviors;
using BookShop.Repository.Common.Validation;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop
{
    /// <summary>
    /// Register Dependency for application layer
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to register application dependency
        /// </summary>
        /// <param name="services"></param>
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidation<,>));
            services.AddTransient(typeof(IRequestPostProcessor<,>), typeof(AuditTrail<,>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            

        }
    }
}