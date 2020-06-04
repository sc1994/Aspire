using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Aspire.EfCore.Test
{
    public class DbContext_Test : DbContext
    {
        public DbContext_Test([NotNull] DbContextOptions<DbContext_Test> options) : base(options)
        {
        }

        public DbSet<EfCoreEntity_Long_Test> EfCoreEntityLongTest { get; set; }

        public DbSet<EfCoreEntity_Guid_Test> EfCoreEntityGuidTest { get; set; }
    }

    public class DbContextOptions_Test : DbContextOptions<DbContext_Test>
    {
        public static DbContextOptions<DbContext_Test> Options
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<DbContext_Test>();
                optionsBuilder.UseSqlite("Data Source = D:/SqliteDbs/aspire_efcore_test.db");
                return optionsBuilder.Options;
            }
        }
    }

    public class DbContextFactory_Test : IDesignTimeDbContextFactory<DbContext_Test>
    {
        public DbContext_Test CreateDbContext(string[] args)
        {
            return new DbContext_Test(DbContextOptions_Test.Options);
        }
    }
}
