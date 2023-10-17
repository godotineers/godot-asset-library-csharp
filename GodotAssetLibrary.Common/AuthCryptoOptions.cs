namespace GodotAssetLibrary.Common
{
    public class AuthCryptoOptions
    {
        public int WorkFactor { get; set; } = 12;

        public string SecretKey { get; set; }

        public int TokenExpirationTime { get; set; }

        public int TokenSessionBytesLength { get; set; }

        public int TokenResetBytesLength { get; set; }
    }
}
