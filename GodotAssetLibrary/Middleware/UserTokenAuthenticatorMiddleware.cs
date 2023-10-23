using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Common.User;
using GodotAssetLibrary.Contracts;
using System.Security.Claims;
using System.Security.Principal;

namespace GodotAssetLibrary.Middleware
{
    public class UserTokenAuthenticatorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenUtility<AuthTokenData> tokenUtility;

        public UserTokenAuthenticatorMiddleware(
                RequestDelegate next,
                ITokenUtility<AuthTokenData> tokenUtility)
        {
            _next = next;
            this.tokenUtility = tokenUtility;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            string tokenString = null;
            if (context.Request.Cookies.TryGetValue("token", out var tokenCookie))
            {
                tokenString = tokenCookie;
            }
            else if (context.Request.Headers.TryGetValue("token", out var tokenHeader))
            {
                tokenString = tokenHeader.FirstOrDefault();
            }

            if (!string.IsNullOrWhiteSpace(tokenString) && TryParseToken(tokenString, out var tokenData))
            {
                // token was provided and is valid.
                context.Items.Add("userData", tokenData.UserData);

                var principal = new GenericPrincipal(new UserDataPrincipal(tokenData.UserData), GetRoles(tokenData.UserData.Type));
                principal.AddIdentity(new ClaimsIdentity(new Claim[] {
                    new Claim("userId", tokenData.UserData.UserId.ToString()),
                    new Claim("email", tokenData.UserData.Email),
                    new Claim("type", tokenData.UserData.Type.ToString()),
                }));

                context.User = principal;
            }

            await _next(context);
        }

        private bool TryParseToken(string tokenString, out AuthTokenData? tokenData)
        {
            tokenData = tokenUtility.Validate(tokenString);
            bool isValid = false;
            if (tokenData != null)
            {
                isValid = tokenData.Session != null && tokenData.UserData != null;
            }

            return isValid;
        }

        private string[] GetRoles(UserType userType)
        {
            return Enum.GetValues<UserType>().Where(x => userType >= x).Select(x => x.ToString()).ToArray();
        }
    }

    internal class UserDataPrincipal : IIdentity
    {
        private UserData userData;

        public UserDataPrincipal(UserData userData)
        {
            this.userData = userData;
        }

        public string? AuthenticationType => "token";

        public bool IsAuthenticated => userData != null;

        public string? Name => userData.Username;
    }

    public static class UserTokenAuthenticatorMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserTokenAuthenticator(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserTokenAuthenticatorMiddleware>();
        }
    }
}
