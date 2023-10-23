using GodotAssetLibrary.Contracts;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace GodotAssetLibrary.Infrastructure.Session
{
    internal class TokenUtility<TToken> : ITokenUtility<TToken>
    {
        public TokenUtility(IOptions<AuthCryptoOptions> options)
        {
            Options = options;
        }

        public IOptions<AuthCryptoOptions> Options { get; }

        public string GenerateToken(TToken tokenDataObj)
        {
            var tokenData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tokenDataObj)));
            var tokenId = new byte[8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenId);
            }

            var tokenTime = Convert.ToBase64String(BitConverter.GetBytes(DateTime.UtcNow.ToBinary()));
            var tokenPayload = $"{tokenData}&{Convert.ToBase64String(tokenId)}|{tokenTime}";
            var token = $"{tokenPayload}&{Convert.ToBase64String(SignToken(tokenPayload))}";

            return token;
        }

        private byte[] SignToken(string tokenPayload)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Options.Value.SecretKey)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenPayload));
            }
        }

        public TToken? Validate(string token)
        {
            var tokenParts = token.Split('&');
            if (tokenParts.Length != 3)
            {
                return default;
            }

            var tokenData = Encoding.UTF8.GetString(Convert.FromBase64String(tokenParts[0]));
            var tokenTime = BitConverter.ToInt64(Convert.FromBase64String(tokenParts[1].Split('|')[1]), 0);
            var tokenSignature = Convert.FromBase64String(tokenParts[2]);

            var tokenPayload = $"{tokenParts[0]}&{tokenParts[1]}";

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(tokenSignature, SignToken(tokenPayload)) ||
                DateTime.UtcNow > DateTime.FromBinary(tokenTime).AddSeconds(Convert.ToDouble(Options.Value.TokenExpirationTime)))
            {
                return default;
            }

            return JsonSerializer.Deserialize<TToken>(tokenData);
        }
    }
}
