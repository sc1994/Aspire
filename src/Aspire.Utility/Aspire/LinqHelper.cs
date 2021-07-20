using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspire
{
    /// <summary>
    ///     linq 帮助类.
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>
        ///     异步获取第一个或者默认值.
        /// </summary>
        /// <param name="source">原始数据.</param>
        /// <param name="predicate">predicate.</param>
        /// <typeparam name="T">数据类型.</typeparam>
        /// <returns>第一个或者默认值.</returns>
        public static async Task<T> FirstOrDefaultAsync<T>(
            this Task<IEnumerable<T>> source,
            Func<T, bool> predicate = null)
        {
            if (source is null) return default;

            var list = await source;
            if (list?.Any() != true) return default;

            if (predicate is null) return list.FirstOrDefault();

            return list.FirstOrDefault(predicate);
        }
    }
}