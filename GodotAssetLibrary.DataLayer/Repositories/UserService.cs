using GodotAssetLibrary.Common.Enums;
using GodotAssetLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace GodotAssetLibrary.DataLayer.Services
{
    internal class UserService : IUserService
    {
        private readonly IAssetLibraryContext _context;

        public UserService(IAssetLibraryContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id, cancellationToken);
        }

        public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username, cancellationToken);
        }

        public async Task<User?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserBySessionToken(byte[] sessionToken, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.SessionToken == sessionToken, cancellationToken);
        }

        public async Task<User?> GetUserByResetToken(byte[] resetToken, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ResetToken == resetToken, cancellationToken);
        }

        public async Task SetSessionToken(int userId, byte[] sessionToken, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.SessionToken = sessionToken;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SetResetToken(int userId, byte[] resetToken, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.ResetToken = resetToken;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SetPasswordAndNullifySession(int userId, string passwordHash, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.SessionToken = null;
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task Register(string username, string email, string passwordHash, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(new User
            {
                Email = email,
                PasswordHash = passwordHash,
                Username = username,
            });

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Promote(int userId, UserType userType, CancellationToken cancellationToken = default)
        {
            var user = await GetUserById(userId, cancellationToken);
            if (user != null)
            {
                user.Type = userType;
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
