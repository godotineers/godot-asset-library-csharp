using GodotAssetLibrary.Contracts.Repositories;
using GodotAssetLibrary.DataLayer.Repositories;
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
            services.AddScoped<IAssetEditService, AssetEditService>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVersionRepository, VersionRepository>();
            services.AddScoped<ILicenseRepository, LicenseRepository>();

            return services;
        }
    }
}
