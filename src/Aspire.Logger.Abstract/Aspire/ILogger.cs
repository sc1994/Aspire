using System;
using System.Runtime.CompilerServices;

namespace Aspire
{
    /// <summary>
    ///     日志.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     info.
        /// </summary>
        /// <param name="message">消息.</param>
        /// <param name="f1">过滤1.</param>
        /// <param name="f2">过滤2.</param>
        /// <param name="f3">过滤3.</param>
        /// <param name="callerFilePath">调用文件路径(无需传入).</param>
        /// <param name="callerMemberName">掉用方法名(无需传入).</param>
        /// <param name="callerLineNumber">调用行数(无需传入).</param>
        void Info(
            string message,
            string f1 = null,
            string f2 = null,
            string f3 = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        ///     warn.
        /// </summary>
        /// <param name="message">消息.</param>
        /// <param name="f1">过滤1.</param>
        /// <param name="f2">过滤2.</param>
        /// <param name="f3">过滤3.</param>
        /// <param name="callerFilePath">调用文件路径(无需传入).</param>
        /// <param name="callerMemberName">掉用方法名(无需传入).</param>
        /// <param name="callerLineNumber">调用行数(无需传入).</param>
        void Warn(
            string message,
            string f1 = null,
            string f2 = null,
            string f3 = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        ///     warn.
        /// </summary>
        /// <param name="exception">异常.</param>
        /// <param name="message">消息.</param>
        /// <param name="f1">过滤1.</param>
        /// <param name="f2">过滤2.</param>
        /// <param name="f3">过滤3.</param>
        /// <param name="callerFilePath">调用文件路径(无需传入).</param>
        /// <param name="callerMemberName">掉用方法名(无需传入).</param>
        /// <param name="callerLineNumber">调用行数(无需传入).</param>
        void Warn(
            Exception exception,
            string message = null,
            string f1 = null,
            string f2 = null,
            string f3 = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        ///     error.
        /// </summary>
        /// <param name="exception">异常.</param>
        /// <param name="message">消息.</param>
        /// <param name="f1">过滤1.</param>
        /// <param name="f2">过滤2.</param>
        /// <param name="f3">过滤3.</param>
        /// <param name="callerFilePath">调用文件路径(无需传入).</param>
        /// <param name="callerMemberName">掉用方法名(无需传入).</param>
        /// <param name="callerLineNumber">调用行数(无需传入).</param>
        void Error(
            Exception exception,
            string message = null,
            string f1 = null,
            string f2 = null,
            string f3 = null,
            [CallerFilePath] string callerFilePath = null,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);
    }
}