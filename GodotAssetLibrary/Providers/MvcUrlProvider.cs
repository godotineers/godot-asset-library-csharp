using GodotAssetLibrary.Contracts;

namespace GodotAssetLibrary.Providers
{
    public class MvcUrlProvider : IUrlProvider
    {
        public MvcUrlProvider(
                    IHttpContextAccessor httpContext,
                    LinkGenerator linkGenerator)
        {
            HttpContext = httpContext;
            LinkGenerator = linkGenerator;
        }

        public IHttpContextAccessor HttpContext { get; }
        public LinkGenerator LinkGenerator { get; }

        public string? GenerateUrl(string action, string controllerName, object routeProperties)
        {
            return LinkGenerator.GetUriByAction(HttpContext.HttpContext, action, controllerName, routeProperties);
        }
    }
}
