using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.Infrastructure.Mail;
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
            services.AddSingleton(typeof(ITokenUtility<>), typeof(TokenUtility<>));
            services.AddSingleton<IPasswordUtility, PasswordUtility>();
            services.AddScoped<IMailUtility, MailUtility>();
            services.AddScoped<ISessionUtility, SessionUtility>();
            services.AddScoped<IRequestLifetime, RequestLifetimeUtility>();

            services.AddOptions<AuthCryptoOptions>()
                .Validate(options => !string.IsNullOrWhiteSpace(options.SecretKey), "Auth Secret key needs to be specified")
                .Validate(options => !string.Equals(options.SecretKey, "somerandomstringshouldbeputhere"), "Auth Secret key needs to be changed from default setting.");

            services.AddOptions<MailOptions>()
                .Validate(options => options.Smtp != null, "SMTP settings need to be specified.")
                .Validate(options => !string.IsNullOrWhiteSpace(options.From), "From address needs to be specified.")
                .Validate(options => !string.IsNullOrWhiteSpace(options.Smtp.Host), "SMTP Host address needs to be specified.")
                .Validate(options => options.Smtp.Port > 0, "SMTP Port needs to be specified.")
                .Validate(options => !options.Smtp.Secure || (options.Smtp.Secure && !string.IsNullOrWhiteSpace(options.Smtp.Auth.User)), "SMTP Auth User need to be specified if secure is true.")
                .Validate(options => !options.Smtp.Secure || (options.Smtp.Secure && !string.IsNullOrWhiteSpace(options.Smtp.Auth.Pass)), "SMTP Auth Pass need to be specified if secure is true.");

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

            public ICommonBuilder ConfigureMailOptions(Action<MailOptions> mailOptions)
            {
                Services.Configure(mailOptions);

                return this;
            }
        }
    }
}
