using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_asset_previews")]
    public class AssetPreview
    {
        [Key]
        [Column("preview_id")]
        public int PreviewId { get; set; }

        [Column("asset_id")]
        public int AssetId { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Required]
        [Column("link")]
        public string Link { get; set; }

        [Required]
        [Column("thumbnail")]
        public string Thumbnail { get; set; }
    }
}
