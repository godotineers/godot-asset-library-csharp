using GodotAssetLibrary.Application.Attributes;
using GodotAssetLibrary.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace GodotAssetLibrary.Application.Middlewares;

public class CachingBehavior<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    public CachingBehavior(IMemoryCache memoryCache)
    {
        MemoryCache = memoryCache;
    }

    private IMemoryCache MemoryCache { get; }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var useCacheAttribute = typeof(TRequest).GetCustomAttribute<UseCacheAttribute>();

        if (useCacheAttribute != null)
        {
            var key = request.GetCacheKey();

            if (MemoryCache.TryGetValue(key, out var cachedValue))
            {
                return (TResponse)cachedValue;
            }

            var response = await next();

            MemoryCache.Set(key, response);
        }

        return await next();
    }
}
