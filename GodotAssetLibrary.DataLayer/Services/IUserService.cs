using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int id);
        Task<User> GetUserByResetToken(byte[] resetToken);
        Task<User> GetUserBySessionToken(byte[] sessionToken);
        Task<User> GetUserByUsername(string username);
        Task SetPasswordAndNullifySession(int userId, string passwordHash);
        Task SetResetToken(int userId, byte[] resetToken);
        Task SetSessionToken(int userId, byte[] sessionToken);
    }
}
