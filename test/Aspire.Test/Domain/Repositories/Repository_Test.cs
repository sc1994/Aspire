using System.Linq;

using Aspire.Domain.Entities;
using Aspire.Domain.Repositories;

using Xunit;

namespace Aspire.Test.Domain.Repositories
{
    public abstract class Repository_Test<TEntity, TId>
        where TEntity : BaseEntity<TId>
    {
        protected abstract IRepository<TEntity, TId> Repository { get; }

        public virtual async void AddAsync_Test()
            => Assert.True(await Repository.AddAsync(CreateEntity()));

        public virtual async void AddThenIdAsync_Test()
            => Assert.NotEqual(default, await Repository.AddThenIdAsync(CreateEntity()));

        public virtual async void AddRangeAsync_Test()
            => Assert.Equal(3, await Repository.AddRangeAsync(CreateEntity(), CreateEntity(), CreateEntity()));

        public virtual async void AddThenEntityAsync_Test()
        {
            var e = CreateEntity();
            var r = await Repository.AddThenEntityAsync(e);
            Assert.Equal(r, e);
        }

        public virtual async void AddRangeThenIdsAsync_Test()
        {
            var e = new[] { CreateEntity(), CreateEntity(), CreateEntity() };
            var r = await Repository.AddRangeThenIdsAsync(e);
            Assert.Equal(e.Length, r.Length);
            Assert.True(r.All(x => !x.Equals(default)));
        }

        public virtual async void AddRangeThenEntitiesAsync_Test()
        {
            var e = new[] { CreateEntity(), CreateEntity(), CreateEntity() };
            var r = await Repository.AddRangeThenEntitiesAsync(e);
            Assert.Equal(e.Length, r.Length);
        }

        public virtual async void DeleteAsync_ById_Test()
            => Assert.True(await Repository.DeleteAsync(await Repository.AddThenIdAsync(CreateEntity())));

        public virtual async void DeleteRangeAsync_ByIds_Test()
            => Assert.Equal(3, await Repository.DeleteRangeAsync(await Repository.AddRangeThenIdsAsync(CreateEntity(), CreateEntity(), CreateEntity())));

        public virtual async void DeleteAsync_ByEntity_Test()
            => Assert.True(await Repository.DeleteAsync(await Repository.AddThenEntityAsync(CreateEntity())));

        public virtual async void DeleteRangeAsync_ByEntities_Test()
            => Assert.Equal(3, await Repository.DeleteRangeAsync(await Repository.AddRangeThenEntitiesAsync(CreateEntity(), CreateEntity(), CreateEntity())));

        public virtual async void UpdateAsync()
        {
            var e = await Repository.AddThenEntityAsync(CreateEntity());
            e.IsDelete = true;
            Assert.True(await Repository.UpdateAsync(e));
        }

        public virtual async void UpdateRangeAsync_Test()
        {
            var e = await Repository.AddRangeThenEntitiesAsync(CreateEntity(), CreateEntity(), CreateEntity());
            e = e.Select(x => { x.IsDelete = true; return x; }).ToArray();
            Assert.Equal(3, await Repository.UpdateRangeAsync(e));
        }

        public virtual async void UpdateRangeThenEntitiesAsync_Test()
        {
            var ids = await Repository.AddRangeThenIdsAsync(CreateEntity(), CreateEntity(), CreateEntity());
            var e = await Repository.GetByIdsAsync(ids);
            e = e.Select(x => { x.IsDelete = true; return x; }).ToArray();
            var r = await Repository.UpdateRangeThenEntitiesAsync(e);
            Assert.True(r.All(x => e.Contains(x)));
            Assert.True(e.All(x => x.IsDelete));
        }

        public virtual async void UpdateThenEntityAsync_Test()
        {
            var e = await Repository.AddThenEntityAsync(CreateEntity());
            e.IsDelete = true;
            Assert.Equal(e, await Repository.UpdateThenEntityAsync(e));
            Assert.True((await Repository.UpdateThenEntityAsync(e)).IsDelete);
        }

        public virtual async void GetByIdAsync_Test()
        {
            var e = await Repository.AddThenEntityAsync(CreateEntity());
            var r = await Repository.GetByIdAsync(e.Id);
            Assert.NotNull(r);
            Assert.Equal(r.Id, e.Id);
        }

        public virtual async void GetByIdsAsync_Test()
        {
            var e = await Repository.AddRangeThenEntitiesAsync(CreateEntity(), CreateEntity(), CreateEntity());
            var r = await Repository.GetByIdsAsync(e.Select(x => x.Id));
            Assert.Equal(3, r.Length);
        }

        public abstract void QuerySqlRawAsync_Test();

        public abstract void ExecuteSqlRawAsync();

        public abstract TEntity CreateEntity();

    }
}
