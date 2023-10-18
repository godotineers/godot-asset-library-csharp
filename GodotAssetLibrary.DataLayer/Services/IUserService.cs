using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default);
        Task<User?> GetUserById(int id, CancellationToken cancellationToken = default);
        Task<User?> GetUserByResetToken(byte[] resetToken, CancellationToken cancellationToken = default);
        Task<User?> GetUserBySessionToken(byte[] sessionToken, CancellationToken cancellationToken = default);
        Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default);
        Task Register(string username, string email, string passwordHash, CancellationToken cancellationToken = default);
        Task SetPasswordAndNullifySession(int userId, string passwordHash, CancellationToken cancellationToken = default);
        Task SetResetToken(int userId, byte[] resetToken, CancellationToken cancellationToken = default);
        Task SetSessionToken(int userId, byte[] sessionToken, CancellationToken cancellationToken = default);
    }
}
