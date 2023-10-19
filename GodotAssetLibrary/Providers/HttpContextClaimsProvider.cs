using GodotAssetLibrary.Contracts;
using System.Security.Claims;

namespace GodotAssetLibrary.Providers
{
    public class HttpContextClaimsProvider : IClaimsProvider
    {
        public HttpContextClaimsProvider(IHttpContextAccessor httpContext)
        {
            ClaimsPrincipal = httpContext.HttpContext?.User;
        }
        public ClaimsPrincipal? ClaimsPrincipal { get; private set; }

    }
}
