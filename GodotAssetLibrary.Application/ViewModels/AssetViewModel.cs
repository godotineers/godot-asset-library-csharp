using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotAssetLibrary.Application.ViewModels
{
    public class AssetViewModel
    {
        public string Id { get; set; }
        public bool Searchable { get; set; }
        public string Title { get; set; }
        public string VersionString { get; set; }
        public string GodotVersion {  get; set; }
        public string Category { get; set; }

        public SupportLevel SupportLevel { get; set; }

        public string Author { get; set; }

        public string Cost { get; set; }

        public string ModifyDate { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string BrowseUrl { get; set; }

        public string DownloadUrl { get; set; }

        public string IssuesUrl { get; set; }

        public IEnumerable<AssetPreviewViewModel> Previews { get; set; } = Array.Empty<AssetPreviewViewModel>();
    }
}
