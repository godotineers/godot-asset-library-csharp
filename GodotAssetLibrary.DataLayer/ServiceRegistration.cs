using GodotAssetLibrary.DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GodotAssetLibrary.DataLayer
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDataLayer(this IServiceCollection services, Action<DbContextOptionsBuilder> contextOptions)
        {
            services.AddDbContext<IAssetLibraryContext, AssetLibraryContext>(options => contextOptions(options));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
