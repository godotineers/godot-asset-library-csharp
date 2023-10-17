namespace GodotAssetLibrary.Application.Results.Auth
{
    public class LoginResult
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public bool Authenticated { get; set; }

        public string Url { get; set; }

        public string Error { get; set; }
    }
}
