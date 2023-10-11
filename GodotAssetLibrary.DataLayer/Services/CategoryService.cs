using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public class CategoryService
    {
        private readonly AssetLibraryContext _context;

        public CategoryService(AssetLibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> ListCategoriesByType(CategoryTypes categoryType)
        {
            return _context.Categories
                           .Where(c => c.CategoryType == categoryType)
                           .OrderBy(c => c.CategoryId)
                           .ToList();
        }
    }
}
