
using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Aspire.EfCore.Test
{
    public class RepositoryEfCore_Guid_Test : RepositoryEfCore_Test<EfCoreEntity_Guid_Test, Guid>
    {
        [Fact]
        public override void AddAsync_Test() => base.AddAsync_Test();
        [Fact]
        public override void AddRangeAsync_Test() => base.AddRangeAsync_Test();
        [Fact]
        public override void AddRangeThenEntitiesAsync_Test() => base.AddRangeThenEntitiesAsync_Test();
        [Fact]
        public override void AddRangeThenIdsAsync_Test() => base.AddRangeThenIdsAsync_Test();
        [Fact]
        public override void AddThenEntityAsync_Test() => base.AddThenEntityAsync_Test();
        [Fact]
        public override void AddThenIdAsync_Test() => base.AddThenIdAsync_Test();
        [Fact]
        public override void DeleteAsync_ByEntity_Test() => base.DeleteAsync_ByEntity_Test();
        [Fact]
        public override void DeleteAsync_ById_Test() => base.DeleteAsync_ById_Test();
        [Fact]
        public override void DeleteRangeAsync_ByEntities_Test() => base.DeleteRangeAsync_ByEntities_Test();
        [Fact]
        public override void DeleteRangeAsync_ByIds_Test() => base.DeleteRangeAsync_ByIds_Test();
        [Fact]
        public override void GetByIdAsync_Test() => base.GetByIdAsync_Test();
        [Fact]
        public override void GetByIdsAsync_Test() => base.GetByIdsAsync_Test();
        [Fact]
        public override void PrimaryKeyNames_Test() => base.PrimaryKeyNames_Test();
        [Fact]
        public override void TableName_Test() => base.TableName_Test();
        [Fact]
        public override void UpdateAsync() => base.UpdateAsync();
        [Fact]
        public override void UpdateAsync_Part_Test() => base.UpdateAsync_Part_Test();
        [Fact]
        public override void UpdateRangeAsync_Part_Test() => base.UpdateRangeAsync_Part_Test();
        [Fact]
        public override void UpdateRangeAsync_Test() => base.UpdateRangeAsync_Test();
        [Fact]
        public override void UpdateRangeThenEntitiesAsync_Test() => base.UpdateRangeThenEntitiesAsync_Test();
        [Fact]
        public override void UpdateThenEntityAsync_Test() => base.UpdateThenEntityAsync_Test();
        [Fact]
        public override async void QuerySqlRawAsync_Test()
        {
            var ids = await Repository.AddRangeThenIdsAsync(CreateEntity(), CreateEntity(), CreateEntity());
            var r = await Repository.QuerySqlRawAsync("SELECT * FROM EfCoreEntityGuidTest WHERE Id IN ({0},{1},{2})", ids.Select(x => (object)x).ToArray());
            Assert.Equal(3, r.Length);
            Assert.True(r.All(x => ids.Contains(x.Id)));
        }
        [Fact]
        public override async void ExecuteSqlRawAsync()
        {
            var ids = await Repository.AddRangeThenIdsAsync(CreateEntity(), CreateEntity(), CreateEntity());
            var paramers = new List<object> { 999 }.Concat(ids.Select(x => (object)x)).ToArray();
            var r = await Repository.ExecuteSqlRawAsync("UPDATE EfCoreEntityGuidTest SET Age = Age + {0} WHERE Id IN ({1},{2},{3})", paramers);
            Assert.Equal(3, r);
        }
    }
}
