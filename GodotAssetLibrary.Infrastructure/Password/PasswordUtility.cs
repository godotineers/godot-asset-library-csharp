using GodotAssetLibrary.Contracts;
using GodotAssetLibrary.Infrastructure;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GodotAssetLibrary.Infrastructure.Password
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
            return BCrypt.Net.BCrypt.HashPassword(initialPassword, workFactor: Options.Value.WorkFactor);
        }

        public bool Verify(string initialPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(initialPassword, passwordHash);
        }


        public byte[] GenerateResetToken()
        {
            byte[] id = new byte[Options.Value.TokenResetBytesLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(id);
            }

            return id;
        }
    }
}
