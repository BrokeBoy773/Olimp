using Olimp.UserManagement.Domain.Entities;

namespace Olimp.UserManagement.Infrastructure.Authentication.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateJwtSecurityToken();
    }
}
