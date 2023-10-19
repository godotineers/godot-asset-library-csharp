using GodotAssetLibrary.Common.Enums;

namespace GodotAssetLibrary.Common.User
{
    public class UserData
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public UserType Type { get; set; }
    }
}
