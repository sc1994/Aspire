using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using Moq;
using Template.Util;
using Xunit;
using Xunit.Abstractions;

namespace Template.UnitTest.Repositories;

public class Repository_Test : Base_Test
{
    public Repository_Test(ITestOutputHelper output) : base(output, Register)
    {
    }

    private static void Register(ContainerBuilder builder)
    {
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task GetAsync_Test(bool hasData)
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.GetListAsync(It.IsAny<IEnumerable<long>>()))
            .Returns(async () =>
            {
                if (hasData)
                {
                    return await Task.FromResult(new List<Test_Entity>
                    {
                        new()
                        {
                            Id = 1
                        }
                    });
                }

                return await Task.FromResult(new List<Test_Entity>());
            });

        var item = await mock.Object.GetAsync(It.IsAny<long>());

        if (hasData)
            Assert.NotNull(item);
        else
            Assert.Null(item);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CreateAsync_Test(bool hasData)
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.CreateBatchAsync(It.IsAny<IEnumerable<Test_Entity>>()))
            .Returns(async () =>
            {
                if (hasData)
                {
                    return await Task.FromResult(new List<Test_Entity>
                    {
                        new()
                        {
                            Id = 1
                        }
                    });
                }

                return await Task.FromResult(new List<Test_Entity>());
            });

        var item = await mock.Object.CreateAsync(new Test_Entity());

        if (hasData)
            Assert.NotNull(item);
        else
            Assert.Null(item);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task UpdateAsync_Test(bool hasData)
    {
        var mock = new Mock<Repository<Test_Entity, long>>(Container);

        mock.Setup(x => x.UpdateBatchAsync(It.IsAny<IEnumerable<Test_Entity>>()))
            .Returns(async () =>
            {
                if (hasData)
                {
                    return await Task.FromResult(new List<Test_Entity>
                    {
                        new()
                        {
                            Id = 1
                        }
                    });
                }

                return await Task.FromResult(new List<Test_Entity>());
            });

        var item = await mock.Object.UpdateAsync(new Test_Entity());

        if (hasData)
            Assert.NotNull(item);
        else
            Assert.Null(item);
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

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TrySetCreated_Test(bool needSet)
    {
        if (needSet)
        {
            var mock = new Mock<Repository<Test_Full_Entity, long>>(Container);
            var list = new Test_Full_Entity[] {new()};
            mock.Object.TrySetCreated(list);
            Assert.Contains(list, x => !string.IsNullOrWhiteSpace(x.CreatedBy));
            Assert.Contains(list, x => x.CreatedAt > Const.DefaultDateTime);
        }
        else
        {
            var mock = new Mock<Repository<Test_Entity, long>>(Container);
            var list = new Test_Entity[] {new()};
            mock.Object.TrySetCreated(list);
            // 没有异常就可以通过
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TrySetUpdated_Test(bool needSet)
    {
        if (needSet)
        {
            var mock = new Mock<Repository<Test_Full_Entity, long>>(Container);
            var list = new Test_Full_Entity[] {new()};
            mock.Object.TrySetUpdated(list);
            Assert.Contains(list, x => !string.IsNullOrWhiteSpace(x.UpdatedBy));
            Assert.Contains(list, x => x.UpdatedAt > Const.DefaultDateTime);
        }
        else
        {
            var mock = new Mock<Repository<Test_Entity, long>>(Container);
            var list = new Test_Entity[] {new()};
            mock.Object.TrySetUpdated(list);
            // 没有异常就可以通过
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void TrySetDeleted_Test(bool needSet)
    {
        if (needSet)
        {
            var mock = new Mock<Repository<Test_Full_Entity, long>>(Container);
            var list = new Test_Full_Entity[] {new()};
            mock.Object.TrySetDeleted(list);
            Assert.Contains(list, x => !string.IsNullOrWhiteSpace(x.DeletedBy));
            Assert.Contains(list, x => x.DeletedAt > Const.DefaultDateTime);
            Assert.Contains(list, x => x.IsDeleted);
        }
        else
        {
            var mock = new Mock<Repository<Test_Entity, long>>(Container);
            var list = new Test_Entity[] {new()};
            mock.Object.TrySetDeleted(list);
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