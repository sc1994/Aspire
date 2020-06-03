using Microsoft.EntityFrameworkCore;

namespace Aspire.EfCore
{
    public interface IDbContextOptionsBuilder<TDbContext>
        where TDbContext : DbContext
    {
    }
}