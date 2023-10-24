using GodotAssetLibrary.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GodotAssetLibrary.Domain
{
    [Table("as_users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("type")]
        public UserType Type { get; set; }

        [Column("session_token")]
        public byte[]? SessionToken { get; set; }

        [Column("reset_token")]
        public byte[]? ResetToken { get; set; }
    }
}
