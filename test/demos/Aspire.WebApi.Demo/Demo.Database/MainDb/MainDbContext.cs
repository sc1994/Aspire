using Demo.Core.Blogs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Demo.Database.MainDb
{
    public class MainDbContext : DbContext
    {
        public DbSet<BlogEntity> Blogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = D:/SqliteDbs/aspire_main_db.db");
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            return new MainDbContext();
        }
    }
}
