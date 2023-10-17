using GodotAssetLibrary.Common.Password;
using GodotAssetLibrary.Common.Session;
using GodotAssetLibrary.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GodotAssetLibrary.Common
{
    public static class ServiceRegistration
    {
        public static ICommonBuilder AddCommonLayer(
            this IServiceCollection services)
        {
            services.AddSingleton<IPasswordUtility, PasswordUtility>();
            services.AddSingleton<ISessionUtility, SessionUtility>();

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
