using System.Collections.Generic;

namespace Aspire.Dto
{
    /// <summary>
    ///     分页输出dto.
    /// </summary>
    /// <typeparam name="TItem">项类型.</typeparam>
    public class PagingOutputDto<TItem>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PagingOutputDto{TItem}" /> class.
        /// </summary>
        /// <param name="totalCount">总计数.</param>
        /// <param name="list">列表.</param>
        public PagingOutputDto(long totalCount, IEnumerable<TItem> list)
        {
            TotalCount = totalCount;
            List = list;
        }

        /// <summary>
        ///     Gets 总计数.
        /// </summary>
        public long TotalCount { get; }

        /// <summary>
        ///     Gets 列表.
        /// </summary>
        public IEnumerable<TItem> List { get; }
    }
}