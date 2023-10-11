using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using System.Text.Json;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.Helpers
{
    public class TokensHelper
    {
        private readonly IConfiguration _configuration;

        public TokensHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private byte[] SignToken(string token)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration["auth:secret"])))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(token));
            }
        }

        public string Generate(object data)
        {
            var tokenData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));
            var tokenId = new byte[8];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(tokenId);
            }

            var tokenTime = Convert.ToBase64String(BitConverter.GetBytes(DateTime.UtcNow.ToBinary()));
            var tokenPayload = $"{tokenData}&{Convert.ToBase64String(tokenId)}|{tokenTime}";
            var token = $"{tokenPayload}&{Convert.ToBase64String(SignToken(tokenPayload))}";

            return token;
        }

        public object Validate(string token)
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
                DateTime.UtcNow > DateTime.FromBinary(tokenTime).AddSeconds(Convert.ToDouble(_configuration["auth:tokenExpirationTime"])))
            {
                return null;
            }

            return JsonSerializer.Deserialize<Token>(tokenData);
        }
    }
}
