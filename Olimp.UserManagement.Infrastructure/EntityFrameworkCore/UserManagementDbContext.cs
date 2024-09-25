using Microsoft.EntityFrameworkCore;
using Olimp.UserManagement.Domain.Entities;

namespace Olimp.UserManagement.Infrastructure.EntityFrameworkCore
{
    public class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
        }
    }
}
