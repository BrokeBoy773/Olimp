using CSharpFunctionalExtensions;
using Olimp.UserManagement.Domain.Entities;

namespace Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces
{
    public interface IUserRepository
    {
        Task<Result> CreateUserAsync(User user, CancellationToken ct);
        Task<Result<User>> GetUserByEmail(string email, CancellationToken ct);
    }
}
