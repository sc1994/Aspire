using System;
using System.Linq;

using Aspire.Domain.Repositories;
using Aspire.Test.Domain.Repositories;

using Xunit;

namespace Aspire.EfCore.Test
{
    public abstract class RepositoryEfCore_Test<TEntity, TId> : Repository_Test<TEntity, TId>
        where TEntity : EfCoreEntity_Test<TId>, new()
    {
        // 避免ef的单例, 每次创建一个新的仓储
        protected override IRepository<TEntity, TId> Repository =>
            new RepositoryEfCore<TEntity, TId>(new DbContext_Test(DbContextOptions_Test.Options));

        protected IRepositoryEfCore<TEntity, TId> RepositoryEfCore => (IRepositoryEfCore<TEntity, TId>)Repository;

        public override TEntity CreateEntity()
            => new TEntity { Name = RandomString, Age = new Random().Next(18, 60) };

        private string RandomString
        {
            get
            {
                var random = new Random();
                var result = string.Empty;
                for (int i = 0; i < 8; i++)
                {
                    result += "suncheng"[random.Next(0, 8)];
                }
                return result;
            }
        }

        public virtual void PrimaryKeyNames_Test()
            => Assert.Equal("Id", Repository.PrimaryKeyNames.First());

        public virtual void TableName_Test()
        {
            Assert.Contains(Repository.TableName, new[] { "EfCoreEntityGuidTest", "EfCoreEntityLongTest" });
        }

        public virtual async void UpdateAsync_Part_Test()
        {
            var id = await Repository.AddThenIdAsync(CreateEntity());
            var r = await RepositoryEfCore.UpdateAsync(x => { x.Age += 100; }, id);
            Assert.True(r);
        }

        public virtual async void UpdateRangeAsync_Part_Test()
        {
            var ids = await Repository.AddRangeThenIdsAsync(CreateEntity(), CreateEntity(), CreateEntity(), CreateEntity());
            var r = await RepositoryEfCore.UpdateRangeAsync(x => { x.Age += 500; }, ids);
            Assert.Equal(4, r);
        }
    }
}
