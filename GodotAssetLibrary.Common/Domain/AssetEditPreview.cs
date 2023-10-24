using GodotAssetLibrary.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_asset_edit_previews")]
    public class AssetEditPreview
    {
        [Key]
        [Column("edit_preview_id")]
        public int EditPreviewId { get; set; }

        [Column("edit_id")]
        public int EditId { get; set; }

        [Column("preview_id")]
        public int PreviewId { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("link")]
        public string Link { get; set; }

        [Column("thumbnail")]
        public string Thumbnail { get; set; }

        [Column("operation")]
        public EditPreviewOperation Operation { get; set; }

        public AssetPreview Preview { get; set; }
    }
}
