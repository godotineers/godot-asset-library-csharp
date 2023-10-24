namespace GodotAssetLibrary.Application.Extensions;

public static class Extensions
{
    public static string GetCacheKey<TRequest>(this TRequest request)
    {
        return string.Concat(typeof(TRequest).Name, "~", System.Text.Json.JsonSerializer.Serialize(request).GetHashCode().ToString("X2"));
    }
}
