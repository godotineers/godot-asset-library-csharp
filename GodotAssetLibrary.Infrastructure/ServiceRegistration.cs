using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.Infrastructure.Password;
using GodotAssetLibrary.Infrastructure.Session;
using Microsoft.Extensions.DependencyInjection;

namespace GodotAssetLibrary.Infrastructure
{
    public static class ServiceRegistration
    {
        public static ICommonBuilder AddInfrastructureLayer(
            this IServiceCollection services)
        {
            services.AddSingleton<ITokenUtility, TokenUtility>();
            services.AddSingleton<IPasswordUtility, PasswordUtility>();
            services.AddScoped<ISessionUtility, SessionUtility>();
            services.AddScoped<IRequestLifetime, RequestLifetimeUtility>();

            services.AddOptions<AuthCryptoOptions>();

            return new CommonBuilder(services);
        }

        public interface ICommonBuilder
        {
            ICommonBuilder ConfigureAuthCryptoOptions(Action<AuthCryptoOptions> passwordCryptoOptions);
        }

        internal class CommonBuilder : ICommonBuilder
        {
            public CommonBuilder(IServiceCollection services)
            {
                Services = services;
            }

            public IServiceCollection Services { get; }

            public ICommonBuilder ConfigureAuthCryptoOptions(Action<AuthCryptoOptions> passwordCryptoOptions)
            {
                Services.Configure(passwordCryptoOptions);

                return this;
            }
        }
    }
}
