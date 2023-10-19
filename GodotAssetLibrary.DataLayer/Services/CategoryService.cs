using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly IAssetLibraryContext _context;

        public CategoryService(IAssetLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> ListCategoriesByType(CategoryTypes categoryType, CancellationToken cancellationToken = default)
        {
            return await _context.Categories
                           .Where(c => c.CategoryType == categoryType)
                           .OrderBy(c => c.CategoryId)
                           .ToListAsync(cancellationToken);
        }
    }
}
