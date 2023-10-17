using GodotAssetLibrary.Contracts;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace GodotAssetLibrary.Common.Session
{
    internal class SessionUtility : ISessionUtility
    {
        public SessionUtility(IOptions<AuthCryptoOptions> options)
        {
            Options = options;
        }

        public IOptions<AuthCryptoOptions> Options { get; }

        public byte[] GenerateSessionId()
        {
            byte[] id = new byte[this.Options.Value.TokenSessionBytesLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(id);
            }

            return id;
        }

        public byte[] GenerateResetId()
        {
            byte[] id = new byte[this.Options.Value.TokenResetBytesLength];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(id);
            }

            return id;
        }

        public string GenerateToken(TokenData tokenDataObj)
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

        public byte[] SignToken(string tokenPayload)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.Options.Value.SecretKey)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(tokenPayload));
            }
        }

        public TokenData? Validate(string token)
        {
            var tokenParts = token.Split('&');
            if (tokenParts.Length != 3)
            {
                return null;
            }

            var tokenData = Encoding.UTF8.GetString(Convert.FromBase64String(tokenParts[0]));
            var tokenTime = BitConverter.ToInt64(Convert.FromBase64String(tokenParts[1].Split('|')[1]), 0);
            var tokenSignature = Convert.FromBase64String(tokenParts[2]);

            var tokenPayload = $"{tokenParts[0]}&{tokenParts[1]}";

            if (!StructuralComparisons.StructuralEqualityComparer.Equals(tokenSignature, SignToken(tokenPayload)) ||
                DateTime.UtcNow > DateTime.FromBinary(tokenTime).AddSeconds(Convert.ToDouble(this.Options.Value.TokenExpirationTime)))
            {
                return null;
            }

            return JsonSerializer.Deserialize<TokenData>(tokenData);
        }
    }
}
