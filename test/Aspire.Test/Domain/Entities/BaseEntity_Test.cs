using System;

using Aspire.Domain.Entities;

using Xunit;

namespace Aspire.Test.Domain.Entities
{
    public class BaseEntity_Test
    {
        [Fact]
        public void Equals_Test()
        {
            var a = new TestEntity { Id = 1 };
            var b = new TestEntity { Id = 1 };
            Assert.True(a == b);
            Assert.True(a.Equals(b));
            Assert.False(a != b);

            a = new TestEntity { Id = 2 };
            Assert.False(a == b);
        }
    }

    public class TestEntity : BaseEntity
    {
        public override long Id { get; set; }
        public override DateTime CreatedAt { get; set; }
        public override DateTime UpdatedAt { get; set; }
        public override bool IsDelete { get; set; }
        public override DateTime DeleteAt { get; set; }
    }
}
