using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Category> ListCategoriesByType(string categoryType)
        {
            return _context.Categories
                           .Where(c => c.CategoryType.Contains(categoryType))
                           .OrderBy(c => c.CategoryId)
                           .ToList();
        }
    }
}
