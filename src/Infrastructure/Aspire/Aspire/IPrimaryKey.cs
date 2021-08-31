using System;

namespace Aspire
{
    /// <summary>
    /// 主键.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型.</typeparam>
    public interface IPrimaryKey<TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        /// <summary>
        ///     Gets or sets 主键.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}
