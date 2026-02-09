using MemberManagement.Domain.Entities;
using MemberManagement.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MemberManagement.Infrastructure.Services
{
    public class LoginService
    {
        private readonly MMSDbContext _context;
        private readonly PasswordHasher<AppUser> _hasher;

        public LoginService(MMSDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<AppUser>();
        }

        public async Task<AppUser> ValidateUserAsync(string username, string password)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Success ? user : null;
        }
    }
}
