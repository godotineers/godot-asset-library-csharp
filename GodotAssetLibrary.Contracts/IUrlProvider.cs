namespace GodotAssetLibrary.Contracts
{
    public interface IUrlProvider
    {
        string GenerateUrl(string action, string controllerName, object routeProperties);
    }
}
