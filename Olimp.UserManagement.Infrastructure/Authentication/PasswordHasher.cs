using Olimp.UserManagement.Infrastructure.Authentication.Interfaces;

namespace Olimp.UserManagement.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHashedPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
        }
    }
}
