using GodotAssetLibrary.Common.Enums;

namespace GodotAssetLibrary.Application.ViewModels
{
    public class AssetPreviewViewModel
    {
        public AssetPreviewType Type { get; set; }
        public string Link { get; set; }

        public string Thumbnail { get; set; }
    }
}
