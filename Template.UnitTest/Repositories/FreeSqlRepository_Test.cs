using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using FreeSql.DataAnnotations;
using Moq;
using Template.Util;
using Xunit;
using Xunit.Abstractions;

namespace Template.UnitTest.Repositories;

public class FreeSqlRepository_Test : Base_Test
{
    public FreeSqlRepository_Test(ITestOutputHelper output) : base(output, Register)
    {
    }

    protected static void Register(ContainerBuilder builder)
    {
        var f0 = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=db0.db")
            .UseAutoSyncStructure(true)
            .Build();
        builder.Register(_ => f0).As<IFreeSql>();

        var f1 = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=db1.db")
            .UseAutoSyncStructure(true)
            .Build<Db1>();
        builder.Register(_ => f1).As<IFreeSql<Db1>>().As<IFreeSql>();

        var f2 = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, @"Data Source=db2.db")
            .UseAutoSyncStructure(true)
            .Build<Db2>();
        builder.Register(_ => f2).As<IFreeSql<Db2>>().As<IFreeSql>();
    }

    private IEnumerable<Test_Guid_Entity> MockEntities(int count)
    {
        for (var i = 0; i < count; i++)
        {
            yield return new Test_Guid_Entity
            {
                Name = Guid.NewGuid().ToString("N")
            };
        }
    }

    private IEnumerable<Test_Guid_Entity> MockExistedEntities(int count)
    {
        return GetRepository<Test_Guid_Entity, Guid>()
            .GetListAsync(x => !string.IsNullOrWhiteSpace(x.Name), limit: count).Result;
    }

    private IRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>()
        where TEntity : class, IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        return typeof(TPrimaryKey).Name switch
        {
            nameof(Int64) => (IRepository<TEntity, TPrimaryKey>) new FreeSqlRepository<Test_Long_Entity, long>(
                Container),
            nameof(Int32) => (IRepository<TEntity, TPrimaryKey>) new FreeSqlRepository<Test_Int_Entity, int>(Container),
            nameof(Guid) =>
                (IRepository<TEntity, TPrimaryKey>) new FreeSqlRepository<Test_Guid_Entity, Guid>(Container),
            _ => null
        } ?? throw new NullReferenceException();
    }

    [Theory]
    [InlineData("Db0")]
    [InlineData("Db1")]
    [InlineData("Db2")]
    public void Construct_Test(string db)
    {
        object? repo = db switch
        {
            "Db0" => new FreeSqlRepository<Test_Guid_Entity, Guid>(Container),
            "Db1" => new FreeSqlRepository<Test_Int_Entity, int>(Container),
            "Db2" => new FreeSqlRepository<Test_Long_Entity, long>(Container),
            _ => null
        };

        Assert.NotNull(repo);
        var freeSqlInstance = repo?.GetType().GetField("FreeSqlInstance")?.GetValue(repo);

        Assert.NotNull(freeSqlInstance);
        Assert.IsAssignableFrom<IFreeSql>(freeSqlInstance);
        Assert.Contains(db.ToLower(), (freeSqlInstance as IFreeSql)?.Ado.ConnectionString ?? string.Empty);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public async Task CreateBatchAsync_Test(int count)
    {
        var repo = GetRepository<Test_Guid_Entity, Guid>();

        var array = MockEntities(count).ToArray();
        var res = await repo.CreateBatchAsync(array);

        Assert.NotNull(res);
        Assert.Equal(count, array.Length);

        foreach (var item in res)
        {
            Assert.NotEqual(default, item.Id);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public async Task DeleteBatchAsync_Test(int count)
    {
        // 确保有数据
        await CreateBatchAsync_Test(count);
        var array = count == 0 ? Array.Empty<Guid>() : MockExistedEntities(count).Select(x => x.Id).ToArray();

        var repo = GetRepository<Test_Guid_Entity, Guid>();

        var res = await repo.DeleteBatchAsync(array);

        Assert.Equal(res, count);
    }

    public async Task<IEnumerable<Test_Int_Entity>> UpdateBatchAsync_Test()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Test_Int_Entity>> GetListAsync_Test()
    {
        throw new NotImplementedException();
    }

    public async Task<Test_Int_Entity> CreateAsync_Test()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync_Test()
    {
        throw new NotImplementedException();
    }

    public async Task<Test_Int_Entity> UpdateAsync_Test()
    {
        throw new NotImplementedException();
    }

    public async Task<Test_Int_Entity> GetAsync_Test()
    {
        throw new NotImplementedException();
    }

    public class Test_Guid_FullEntity : FullEntity<Guid>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Test_Guid_Entity : IPrimaryKey<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public interface Db1
    {
    }

    public interface Db2
    {
    }

    public class Test_Long_Entity : IPrimaryKey<long>, IDatabase<Db2>
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }

    public class Test_Int_Entity : IPrimaryKey<int>, IDatabase<Db1>
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}