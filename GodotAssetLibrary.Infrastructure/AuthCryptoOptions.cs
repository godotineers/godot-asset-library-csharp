namespace GodotAssetLibrary.Infrastructure
{
    public class AuthCryptoOptions
    {
        public int WorkFactor { get; set; } = 12;

        public string SecretKey { get; set; }

        public int TokenExpirationTime { get; set; } = 15552000;

        public int TokenSessionBytesLength { get; set; } = 24;

        public int TokenResetBytesLength { get; set; } = 32;
    }
}
