using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public class CategoryService
    {
        private readonly IAssetLibraryContext _context;

        public CategoryService(IAssetLibraryContext context)
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
