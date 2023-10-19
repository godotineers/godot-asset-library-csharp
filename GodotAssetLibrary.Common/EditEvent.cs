using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Common
{
    public class EditEvent
    {
        public int EditId { get; set; }

        public int AssetId { get; set; }

        public string Title { get; set; }

        public string VersionString { get; set; }

        public string IconUrl { get; set; }

        public string Status { get; set; }

        public string Reason { get; set; }

        public DateTime SubmitDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public string Category { get; set; }
    }
}
