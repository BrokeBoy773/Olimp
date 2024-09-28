using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Olimp.UserManagement.Infrastructure.Authentication.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Olimp.UserManagement.Infrastructure.Authentication
{
    public class JwtProvider(IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string GenerateJwtSecurityToken()
        {
            SigningCredentials signingCredentials = new(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)), SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));

            string jwtSecurityTokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return jwtSecurityTokenString;
        }
    }
}
