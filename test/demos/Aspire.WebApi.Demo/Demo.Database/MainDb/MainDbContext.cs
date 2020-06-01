using Demo.Core.Blogs;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Database.MainDb
{
    public class MainDbContext : DbContext
    {
        public MainDbContext([NotNull] DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        public DbSet<BlogEntity> BlogEntities { get; set; }
    }

    public class MainDbContextOptionsBuilder : DbContextOptionsBuilder<MainDbContext>
    {
        public override DbContextOptions<MainDbContext> Options
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();
                optionsBuilder.UseSqlite(@"Data Source = C:\Users\suncheng\Desktop\Aspire\test\demos\Aspire.WebApi.Demo\Demo.Application\App_Data\aspire_main_db.db");
                return optionsBuilder.Options;
            }
        }
    }

    public class MainDbDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
    {
        public MainDbContext CreateDbContext(string[] args)
        {
            return new MainDbContext(MainDbContextOptionsBuilder.Instance);
        }
    }
}
