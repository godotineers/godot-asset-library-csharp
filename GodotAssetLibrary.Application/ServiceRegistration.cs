using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GodotAssetLibrary.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMemoryCache();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Middlewares.CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Middlewares.ValidationBehavior<,>));

            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
