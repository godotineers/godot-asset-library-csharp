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

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserBySessionToken(byte[] sessionToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.SessionToken == sessionToken);
        }

        public async Task<User> GetUserByResetToken(byte[] resetToken)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ResetToken == resetToken);
        }

        public async Task SetSessionToken(int userId, byte[] sessionToken)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.SessionToken = sessionToken;
                _context.SaveChanges();
            }
        }

        public async Task SetResetToken(int userId, byte[] resetToken)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.ResetToken = resetToken;
                _context.SaveChanges();
            }
        }

        public async Task SetPasswordAndNullifySession(int userId, string passwordHash)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.SessionToken = null;
                _context.SaveChanges();
            }
        }
    }
}
