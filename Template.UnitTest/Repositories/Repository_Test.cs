using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Moq;
using Template.Entity;
using Xunit;
using Xunit.Abstractions;
using Template.Util;

namespace Template.UnitTest.Repositories;

public class Repository_Test : Base_Test
{
    public Repository_Test(ITestOutputHelper output) : base(output, Register)
    {
    }

    private static void Register(ContainerBuilder builder)
    {
    }

    [Fact]
    public async Task GetAsync_Test()
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.GetListAsync(It.IsAny<IEnumerable<long>>()))
            .Returns(async () => await Task.FromResult(new List<Test_Entity>
            {
                new()
                {
                    Id = 1
                }
            }));

        var item = await mock.Object.GetAsync(It.IsAny<long>());

        Assert.Equal(1, item?.Id);
    }

    [Fact]
    public async Task CreateAsync_Test()
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.CreateBatchAsync(It.IsAny<IEnumerable<Test_Entity>>()))
            .Returns(async () => await Task.FromResult(new List<Test_Entity>
            {
                new()
                {
                    Id = 1
                }
            }));

        var item = await mock.Object.CreateAsync(new Test_Entity());

        Assert.Equal(1, item.Id);
    }

    [Fact]
    public async Task UpdateAsync_Test()
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.UpdateBatchAsync(It.IsAny<IEnumerable<Test_Entity>>()))
            .Returns(async () => await Task.FromResult(new List<Test_Entity>
            {
                new()
                {
                    Id = 1
                }
            }));

        var item = await mock.Object.UpdateAsync(new Test_Entity());

        Assert.NotNull(item);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteAsync_Test(bool hasData)
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.DeleteBatchAsync(It.IsAny<IEnumerable<long>>()))
            .Returns(async () =>
            {
                if (hasData)
                {
                    return await Task.FromResult(1);
                }

                return await Task.FromResult(0);
            });

        var item = await mock.Object.DeleteAsync(It.IsAny<long>());

        if (hasData)
            Assert.True(item);
        else
            Assert.False(item);
    }

    private (Repository<T, long> repo, T[] list) MockEntities<T>(bool isEmpty)
        where T : IPrimaryKey<long>, new()
    {
        var repo = new Mock<Repository<T, long>>(Container);

        if (isEmpty)
            return (repo.Object, Array.Empty<T>());

        return (repo.Object, new T[] {new()});
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void TrySetCreated_Test(bool needSet, bool isEmpty)
    {
        if (needSet)
        {
            var (repo, list) = MockEntities<Test_Full_Entity>(isEmpty);
            repo.TrySetCreated(list);
            Assert.True(list.All(x => !string.IsNullOrWhiteSpace(x.CreatedBy)));
            Assert.True(list.All(x => x.CreatedAt > Const.DefaultDateTime));
        }
        else
        {
            var (repo, list) = MockEntities<Test_Entity>(isEmpty);
            repo.TrySetCreated(list);
            // 没有异常就可以通过
        }
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void TrySetUpdated_Test(bool needSet, bool isEmpty)
    {
        if (needSet)
        {
            var (repo, list) = MockEntities<Test_Full_Entity>(isEmpty);
            repo.TrySetUpdated(list);
            Assert.True(list.All(x => !string.IsNullOrWhiteSpace(x.UpdatedBy)));
            Assert.True(list.All(x => x.UpdatedAt > Const.DefaultDateTime));
        }
        else
        {
            var (repo, list) = MockEntities<Test_Entity>(isEmpty);
            repo.TrySetUpdated(list);
            // 没有异常就可以通过
        }
    }

    [Theory]
    [InlineData(true, false)]
    [InlineData(true, true)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void TrySetDeleted_Test(bool needSet, bool isEmpty)
    {
        if (needSet)
        {
            var (repo, list) = MockEntities<Test_Full_Entity>(isEmpty);
            repo.TrySetDeleted(list);
            Assert.True(list.All(x => !string.IsNullOrWhiteSpace(x.DeletedBy)));
            Assert.True(list.All(x => x.DeletedAt > Const.DefaultDateTime));
            Assert.True(list.All(x => x.IsDeleted));
        }
        else
        {
            var (repo, list) = MockEntities<Test_Entity>(isEmpty);
            repo.TrySetDeleted(list);
            // 没有异常就可以通过
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void IsDeleted_Test(bool @is)
    {
        if (@is)
        {
            var mock = new Mock<Repository<Test_Full_Entity, long>>(Container);
            Assert.True(mock.Object.IsDeleted());
        }
        else
        {
            var mock = new Mock<Repository<Test_Entity, long>>(Container);
            Assert.False(mock.Object.IsDeleted());
        }
    }

    public class Test_Entity : IPrimaryKey<long>
    {
        public long Id { get; set; }
    }

    public class Test_Full_Entity : FullEntity<long>
    {
    }
}