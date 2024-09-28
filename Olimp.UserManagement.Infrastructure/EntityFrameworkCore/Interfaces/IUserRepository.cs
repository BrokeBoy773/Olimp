using CSharpFunctionalExtensions;
using Olimp.UserManagement.Domain.Entities;

namespace Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces
{
    public interface IUserRepository
    {
        Task<Result> CreateUserAsync(User user, CancellationToken ct);

        Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken ct);

        Task<Result<(List<string> ExistingEmails, List<string> ExistingPhoneNumbers), string>> GetEmailsAndPhoneNumbersAsync(CancellationToken ct);
    }
}
