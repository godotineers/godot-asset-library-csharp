using Microsoft.AspNetCore.Mvc.Rendering;

namespace GodotAssetLibrary.ViewModels
{
    public class AssetFieldsViewModel<TModel>
    {
        public TModel AssetModel { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Licenses { get; set; }
        public IEnumerable<SelectListItem> GodotVersions { get; set; }
    }
}
