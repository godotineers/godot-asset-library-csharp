using System.Linq;
using Microsoft.EntityFrameworkCore;
using GodotAssetLibrary.Domain;

namespace GodotAssetLibrary.DataLayer.Services
{
    public class UserService
    {
        private readonly AssetLibraryContext _context;

        public UserService(AssetLibraryContext context)
        {
            _context = context;
        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == id);
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.SingleOrDefault(u => u.Username == username);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserBySessionToken(byte[] sessionToken)
        {
            return _context.Users.SingleOrDefault(u => u.SessionToken == sessionToken);
        }

        public User GetUserByResetToken(byte[] resetToken)
        {
            return _context.Users.SingleOrDefault(u => u.ResetToken == resetToken);
        }

        public void SetSessionToken(int userId, byte[] sessionToken)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.SessionToken = sessionToken;
                _context.SaveChanges();
            }
        }

        public void SetResetToken(int userId, byte[] resetToken)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.ResetToken = resetToken;
                _context.SaveChanges();
            }
        }

        public void SetPasswordAndNullifySession(int userId, string passwordHash)
        {
            var user = _context.Users.Find(userId);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.SessionToken = null;
                _context.SaveChanges();
            }
        }
    }
}
