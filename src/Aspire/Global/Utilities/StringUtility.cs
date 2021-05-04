// <copyright file="StringUtility.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    /// <summary>
    /// String Utility.
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// 是 null 或 空字符串.
        /// </summary>
        /// <param name="str">字符串.</param>
        /// <returns>是否是空字符串.</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 安全的 Substring.
        /// </summary>
        /// <param name="str">string.</param>
        /// <param name="count">count.</param>
        /// <returns>Substring.</returns>
        public static string SubstringSafe(this string str, int count)
        {
            return str.Length > count
                ? str.Substring(0, count)
                : str;
        }
    }
}
