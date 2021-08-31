using System;

namespace Aspire.Entity
{
    /// <inheritdoc />
    public interface IEntityBase<TPrimaryKey, TDatabase> : IEntityBase<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
    }
}