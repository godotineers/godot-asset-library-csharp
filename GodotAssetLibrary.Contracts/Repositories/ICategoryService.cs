using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

        Task<IEnumerable<Category>> ListCategoriesByType(CategoryTypes categoryType, CancellationToken cancellationToken = default);
    }
}
