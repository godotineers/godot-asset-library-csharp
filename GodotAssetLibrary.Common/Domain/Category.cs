using GodotAssetLibrary.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Required]
        [Column("category")]
        public string CategoryName { get; set; }

        [Column("category_type")]
        public CategoryTypes CategoryType { get; set; }
    }
}
