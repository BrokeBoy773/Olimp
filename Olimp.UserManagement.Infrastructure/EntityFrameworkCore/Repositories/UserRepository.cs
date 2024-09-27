using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Olimp.UserManagement.Domain.Entities;
using Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;

namespace Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Repositories
{
    public class UserRepository(UserManagementDbContext dbContext) : IUserRepository
    {
        private readonly UserManagementDbContext _dbContext = dbContext;

        public async Task<Result> CreateUserAsync(User user, CancellationToken ct)
        {
            if (user is null)
                return Result.Failure("User is null");

            await _dbContext.AddAsync(user, ct);
            await _dbContext.SaveChangesAsync(ct);

            return Result.Success();
        }

        public async Task<Result<User>> GetUserByEmail(string email, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<User>("email is null or white space");

            User? user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.EmailAddress == email, ct);

            if (user is null)
                return Result.Failure<User>("User not found");

            return Result.Success(user);
        }
    }
}
