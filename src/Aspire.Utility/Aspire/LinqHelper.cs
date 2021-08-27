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
        public static async Task<T?> FirstOrDefaultAsync<T>(
            this Task<IEnumerable<T>> source,
            Func<T, bool>? predicate = null)
        {
            if (source is null) return default;

            var list = await source;
            if (list?.Any() != true) return default;

            if (predicate is null) return list.FirstOrDefault();

            return list.FirstOrDefault(predicate);
        }

        /// <summary>
        ///     对 数组进行遍历.
        /// </summary>
        /// <param name="source">数组.</param>
        /// <param name="action">处理每一项的方法.</param>
        /// <typeparam name="T">数组项类型.</typeparam>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source) action(item);
        }

        /// <summary>
        ///     where if.
        /// </summary>
        /// <param name="source">源.</param>
        /// <param name="isWhere">是否 过滤, 如果为true 才会执行后面的过滤动作.</param>
        /// <param name="predicate">过滤.</param>
        /// <typeparam name="T">泛型.</typeparam>
        /// <returns>结果.</returns>
        public static IEnumerable<T> WhereIf<T>(
            this IEnumerable<T> source,
            bool isWhere,
            Func<T, bool> predicate)
        {
            if (!isWhere) return source;

            return source.Where(predicate);
        }
    }
}