using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Demo.Database.UserIdentity
{
    public class UserIdentityDbContext : IdentityDbContext
    {
        public UserIdentityDbContext()
        {
        }

        public UserIdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = D:/SqliteDbs/aspire_user_identity_db.db");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class UserIdentityDbContextFactory : IDesignTimeDbContextFactory<UserIdentityDbContext>
    {
        public UserIdentityDbContext CreateDbContext(string[] args)
        {
            return new UserIdentityDbContext();
        }
    }
}
