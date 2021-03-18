// <copyright file="FriendlyThrowException.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System.Diagnostics;

    /// <summary>
    /// 友好的异常抛出.
    /// </summary>
    public static class FriendlyThrowException
    {
        /// <summary>
        /// 抛出异常.
        /// </summary>
        /// <param name="messages">Messages.</param>
        /// <returns>Friendly Exception.</returns>
        public static FriendlyException ThrowException(params string[] messages)
        {
            return ThrowException(ResponseCode.InternalServerError, messages);
        }

        /// <summary>
        /// 抛出异常.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="title">Title.</param>
        /// <param name="messages">Messages.</param>
        /// <returns>Friendly Exception.</returns>
        public static FriendlyException ThrowException(int code, string title, params string[] messages)
        {
            return new FriendlyException(code, EnhancedStackTrace.Current(), title, messages);
        }

        /// <summary>
        /// 抛出异常.
        /// </summary>
        /// <param name="code">Code.</param>
        /// <param name="messages">Messages.</param>
        /// <returns>Friendly Exception.</returns>
        public static FriendlyException ThrowException(ResponseCode code, params string[] messages)
        {
            return ThrowException(code.GetHashCode(), code.GetDescription(), messages);
        }
    }
}
