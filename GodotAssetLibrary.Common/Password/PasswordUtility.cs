using GodotAssetLibrary.Contracts;
using Microsoft.Extensions.Options;

namespace GodotAssetLibrary.Common.Password
{
    internal class PasswordUtility : IPasswordUtility
    {
        public PasswordUtility(IOptions<AuthCryptoOptions> options)
        {
            Options = options;
        }

        public IOptions<AuthCryptoOptions> Options { get; }

        public string Generate(string initialPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(initialPassword, workFactor: this.Options.Value.WorkFactor);
        }

        public bool Verify(string initialPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(initialPassword, passwordHash);
        }
    }
}
