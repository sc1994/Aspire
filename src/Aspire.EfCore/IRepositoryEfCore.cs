using Aspire.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aspire.Domain.Repositories
{
    public interface IRepositoryEfCore<TEntity> : IRepositoryEfCore<TEntity, long>
         where TEntity : BaseEntity
    {

    }

    public interface IRepositoryEfCore<TEntity, TId> : IRepository<TEntity, TId>
         where TEntity : BaseEntity<TId>
    {
        DbSet<TEntity> Query { get; }

        IEntityType EntityType { get; }

        Task<bool> UpdateAsync(Action<TEntity> updater, TId id);

        Task<int> UpdateRangeAsync(Action<TEntity> updater, params TId[] ids);

        Task<int> UpdateRangeAsync(Action<TEntity> updater, IEnumerable<TId> ids);
    }
}
