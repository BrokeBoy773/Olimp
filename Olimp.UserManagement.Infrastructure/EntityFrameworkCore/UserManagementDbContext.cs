using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Olimp.UserManagement.Domain.Entities;
using Serilog;

namespace Olimp.UserManagement.Infrastructure.EntityFrameworkCore
{
    public class UserManagementDbContext(IConfiguration configuration) : DbContext
    {
        private readonly IConfiguration _configuration = configuration;

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString("UserManagementDbContext"))
                .UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddSerilog()))
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
        }
    }
}
