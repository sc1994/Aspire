using System.Runtime.CompilerServices;

using Aspire.Helpers;

using Serilog.Events;

namespace Aspire.SeriLogger
{
    /// <inheritdoc />
    public class SeriLogger : ILogger
    {
        private readonly ILogTracer logTracer;
        private readonly Serilog.ILogger logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SeriLogger" /> class.
        /// </summary>
        /// <param name="logger">Serilog logger.</param>
        /// <param name="logTracer">日志追踪类.</param>
        public SeriLogger(Serilog.ILogger logger, ILogTracer logTracer)
        {
            this.logger = logger;
            this.logTracer = logTracer;
        }

        /// <inheritdoc />
        public void Debug(string message, string? f1 = null, string? f2 = null, string? f3 = null, [CallerFilePath] string? callerFilePath = null, [CallerMemberName] string? callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0)
        {
            Log(
               LogEventLevel.Debug,
               callerFilePath,
               callerMemberName,
               callerLineNumber,
               f1,
               f2,
               f3,
               message,
               null);
        }

        /// <inheritdoc />
        public void Info(
            string message,
            string? f1 = null,
            string? f2 = null,
            string? f3 = null,
            string? callerFilePath = null,
            string? callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogEventLevel.Information,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                f3,
                message,
                null);
        }

        /// <inheritdoc />
        public void Warn(
            string message,
            string? f1 = null,
            string? f2 = null,
            string? f3 = null,
            string? callerFilePath = null,
            string? callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogEventLevel.Warning,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                f3,
                message,
                null);
        }

        /// <inheritdoc />
        public void Warn(
            Exception exception,
            string? message = null,
            string? f1 = null,
            string? f2 = null,
            string? f3 = null,
            string? callerFilePath = null,
            string? callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogEventLevel.Warning,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                f3,
                message,
                exception);
        }

        /// <inheritdoc />
        public void Error(
            Exception exception,
            string? message = null,
            string? f1 = null,
            string? f2 = null,
            string? f3 = null,
            string? callerFilePath = null,
            string? callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                LogEventLevel.Error,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                f3,
                message,
                exception);
        }

        private void Log(
            LogEventLevel level,
            string? callerFilePath,
            string? callerMemberName,
            int callerLineNumber,
            string? f1,
            string? f2,
            string? f3,
            string? message,
            Exception? exception)
        {
            const string template = "[{ms,4}]ms [{traceId}] [{f1,16}][{f2,16}][{f3,16}]\r\n\t{message}\r\n\tat [{callerMemberName}] in [{callerFilePath}]:line {callerLineNumber}";

            var @params = new object?[]
            {
                DateTime.Now.ToTimestamp() - logTracer.CreatedAt,
                logTracer.TraceId,
                f1,
                f2,
                f3,
                message,
                callerMemberName,
                callerFilePath,
                callerLineNumber
            };

            if (exception == null)
            {
                logger.Write(level, template, @params);
            }
            else
            {
                logger.Write(level, exception, template, @params);
            }
        }
    }
}