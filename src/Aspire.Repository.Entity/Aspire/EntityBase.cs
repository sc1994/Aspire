namespace Aspire
{
    /// <inheritdoc />
    public abstract class EntityBase : EntityBase<long>
    {
    }

    /// <summary>
    ///     实体基类.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public abstract class EntityBase<TPrimaryKey>
    {
        /// <summary>
        ///     Gets or sets 主键.
        /// </summary>
        public abstract TPrimaryKey Id { get; set; }
    }
}