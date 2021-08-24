using System;
using static Newtonsoft.Json.JsonConvert;

namespace Aspire
{
    /// <summary>
    ///     转换 帮助类.
    /// </summary>
    public static class ConvertHelper
    {
        private static readonly DateTime StandardDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     将 json 字符串转为 obj.
        /// </summary>
        /// <param name="json">json string.</param>
        /// <typeparam name="T">目标类型.</typeparam>
        /// <returns>目标数据.</returns>
        public static T ToObjByJson<T>(this string json)
        {
            return DeserializeObject<T>(json);
        }

        /// <summary>
        ///     将 <typeparamref name="T"/> 转为 json 字符串.
        /// </summary>
        /// <param name="obj">原始数据.</param>
        /// <typeparam name="T">原始数据类型.</typeparam>
        /// <returns>json string.</returns>
        public static string ToJsonString<T>(this T obj)
            where T : class
        {
            return SerializeObject(obj);
        }

        /// <summary>
        /// 将 <paramref name="dateTime"/> 转为 时间戳.
        /// </summary>
        /// <param name="dateTime">date time.</param>
        /// <returns>时间戳.</returns>
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long)Math.Round((dateTime - StandardDateTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
        }
    }
}