using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Olimp.UserManagement.Application.Interfaces;
using Olimp.UserManagement.Application.Services;
using Olimp.UserManagement.Infrastructure.Authentication;
using Olimp.UserManagement.Infrastructure.Authentication.Interfaces;
using Olimp.UserManagement.Infrastructure.EntityFrameworkCore;
using Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Interfaces;
using Olimp.UserManagement.Infrastructure.EntityFrameworkCore.Repositories;
using System.Text;

namespace Olimp.API.Extensions
{
    public static class CustomServiceCollectionExtensions
    {
        public static void AddUserManagementDbContext(this IServiceCollection services)
        {
            services.AddDbContext<UserManagementDbContext>();
        }

        public static void ConfigureJwtOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>()!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["cookies"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }
}
