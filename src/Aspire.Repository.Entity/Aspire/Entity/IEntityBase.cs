namespace Aspire.Entity
{
    /// <inheritdoc />
    public interface IEntityBase : IEntityBase<long>
    {
    }

    /// <summary>
    ///     实体基类.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public interface IEntityBase<TPrimaryKey>
    {
        /// <summary>
        ///     Gets or sets 主键.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}