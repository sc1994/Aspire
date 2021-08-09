﻿using System;
using Microsoft.Extensions.Logging;

namespace Aspire
{
    /// <inheritdoc />
    public class DefaultLogger : ILogger
    {
        private readonly ILogger<DefaultLogger> logger;
        private readonly IlogTracer logTracer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultLogger"/> class.
        /// 默认的日志纪录类, 使用asp.net的日志工具输出日志.
        /// </summary>
        /// <param name="logger">asp.net logger.</param>
        /// <param name="logTracer">日志追踪类.</param>
        public DefaultLogger(ILogger<DefaultLogger> logger, IlogTracer logTracer)
        {
            this.logger = logger;
            this.logTracer = logTracer;
        }

        /// <inheritdoc />
        public void Info(
            string message,
            string f1 = null,
            string f2 = null,
            string callerFilePath = null,
            string callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogLevel.Information,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                null);
        }

        /// <inheritdoc />
        public void Warn(
            string message,
            string f1 = null,
            string f2 = null,
            string callerFilePath = null,
            string callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogLevel.Warning,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                null);
        }

        /// <inheritdoc />
        public void Warn(
            Exception exception,
            string message = null,
            string f1 = null,
            string f2 = null,
            string callerFilePath = null,
            string callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogLevel.Warning,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                exception);
        }

        /// <inheritdoc />
        public void Error(
            Exception exception,
            string message = null,
            string f1 = null,
            string f2 = null,
            string callerFilePath = null,
            string callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogLevel.Error,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                exception);
        }

        private void Log(
            LogLevel level,
            string callerFilePath,
            string callerMemberName,
            int callerLineNumber,
            string f1,
            string f2,
            string message,
            Exception exception)
        {
            logger.Log(
                level,
                "[{traceId}][{callerFilePath}][{callerMemberName}][{callerLineNumber}] [{f1}][{f2}] {message} {exception}",
                logTracer.TraceId,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                exception?.ToString());
        }
    }
}