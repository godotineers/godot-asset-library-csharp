using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Common.User;
using GodotAssetLibrary.Contracts;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace GodotAssetLibrary.Infrastructure.Session
{
    internal class SessionUtility : ISessionUtility
    {
        public SessionUtility(
                    IOptions<AuthCryptoOptions> options,
                    IClaimsProvider claimsProvider)
        {
            Options = options;
            ClaimsProvider = claimsProvider;
        }

        public IOptions<AuthCryptoOptions> Options { get; }
        public IClaimsProvider ClaimsProvider { get; }

        public byte[] GenerateSessionId()
        {
            byte[] id = new byte[Options.Value.TokenSessionBytesLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(id);
            }

            return id;
        }

        public byte[] GenerateResetId()
        {
            byte[] id = new byte[Options.Value.TokenResetBytesLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(id);
            }

            return id;
        }

        public UserData GetUserData()
        {
            var principal = ClaimsProvider.ClaimsPrincipal;

            return new UserData
            {
                Email = principal.Claims.First(x => x.Type == "email").Value,
                Type = Enum.Parse<UserType>(principal.Claims.First(x => x.Type == "type").Value),
                UserId = int.Parse(principal.Claims.First(x => x.Type == "userId").Value),
                Username = principal.Identity.Name,
            };
        }
    }
}
