using System.Text.Json.Serialization;

namespace GodotAssetLibrary.Models
{
    public class AuthLoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
    }
}
