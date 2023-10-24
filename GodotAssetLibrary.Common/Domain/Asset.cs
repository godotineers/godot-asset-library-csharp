using GodotAssetLibrary.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_assets")]
    public class Asset
    {
        [Key]
        [Column("asset_id")]
        public int AssetId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("godot_version")]
        public int GodotVersion { get; set; }

        [Column("version")]
        public int Version { get; set; }

        [Required]
        [Column("version_string")]
        public string VersionString { get; set; }

        [Column("cost")]
        public string Cost { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("support_level")]
        public SupportLevel SupportLevel { get; set; }

        [Column("download_provider")]
        public DownloadProvider DownloadProvider { get; set; }

        [Required]
        [Column("download_commit")]
        public string DownloadCommit { get; set; }

        [Required]
        [Column("browse_url")]
        public string BrowseUrl { get; set; }

        [Required]
        [Column("issues_url")]
        public string IssuesUrl { get; set; }

        [Required]
        [Column("icon_url")]
        public string IconUrl { get; set; }

        [Column("searchable")]
        public bool Searchable { get; set; }

        [Column("modify_date")]
        public DateTime ModifyDate { get; set; }

        public User User { get; set; }

        public Category Category { get; set; }

        public AssetPreview Preview { get; set; }
    }
}
