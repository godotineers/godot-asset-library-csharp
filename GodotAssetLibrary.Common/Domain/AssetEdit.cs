using GodotAssetLibrary.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_asset_edits")]
    public class AssetEdit
    {
        [Key]
        [Column("edit_id")]
        public int EditId { get; set; }

        [Column("asset_id")]
        public int AssetId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description", TypeName = "text")]
        public string Description { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("godot_version")]
        public int GodotVersion { get; set; }

        [Column("version_string")]
        public string VersionString { get; set; }

        [Column("cost")]
        public string Cost { get; set; }

        [Column("download_provider")]
        public DownloadProvider DownloadProvider { get; set; }

        [Column("download_commit")]
        public string DownloadCommit { get; set; }

        [Column("browse_url")]
        public string BrowseUrl { get; set; }

        [Column("issues_url")]
        public string IssuesUrl { get; set; }

        [Column("icon_url")]
        public string IconUrl { get; set; }

        [Column("status")]
        public EditStatus Status { get; set; }

        [Required]
        [Column("reason", TypeName = "text")]
        public string Reason { get; set; }

        [Column("submit_date")]
        public DateTime SubmitDate { get; set; }

        [Column("modify_date")]
        public DateTime ModifyDate { get; set; }

        public User User { get; set; }
        public AssetEditPreview AssetEditPreviews { get; set; }
        public Category Category { get; set; }
        public Asset Asset { get; set; }
    }
}
