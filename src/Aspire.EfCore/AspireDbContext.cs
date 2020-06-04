using Microsoft.EntityFrameworkCore;

namespace Aspire.EfCore
{
    public abstract class AspireDbContext<TDbContext> : DbContext
        where TDbContext : DbContext
    {
        private static DbContextOptions<TDbContext> _options;

        protected AspireDbContext() : base(_options)
        {
        }

        public abstract DbContextOptions<TDbContext> Options { get; }
    }
}