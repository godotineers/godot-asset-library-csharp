using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.Application.Results.Core
{
    public class GetCategoriesResult
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
