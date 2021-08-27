using System;

namespace Aspire.Entity
{
    /// <summary>
    ///     实体基类.
    /// </summary>
    public interface IEntityBase : IEntityBase<Guid>
    {
    }

    /// <summary>
    ///     实体基类.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public interface IEntityBase<TPrimaryKey> : IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}