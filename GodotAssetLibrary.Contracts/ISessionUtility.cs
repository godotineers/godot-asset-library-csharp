

using GodotAssetLibrary.Common.User;

namespace GodotAssetLibrary.Contracts
{
    public interface ISessionUtility
    {
        byte[] GenerateSessionId();

        UserData GetUserData();
    }
}
