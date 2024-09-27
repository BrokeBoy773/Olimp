namespace Olimp.UserManagement.Infrastructure.Authentication.Interfaces
{
    public interface IPasswordHasher
    {
        string GenerateHashedPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
