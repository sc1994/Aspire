using System;
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
        public void Info(
            string message,
            string f1 = null,
            string f2 = null,
            string callerFilePath = null,
            string callerMemberName = null,
            int callerLineNumber = 0)
        {
            Log(
                2,
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
                3,
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
                3,
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
                4,
                callerFilePath,
                callerMemberName,
                callerLineNumber,
                f1,
                f2,
                message,
                exception);
        }

        private void Log(
            int level,
            string callerFilePath,
            string callerMemberName,
            int callerLineNumber,
            string f1,
            string f2,
            string message,
            Exception exception)
        {
            const string template =
                "[{traceId}] [{ms}]ms [{f1}][{f2}]\r\n\tat [{callerMemberName}] from [{callerFilePath}] in [{callerLineNumber}]line\r\n\t{message} ";

            var @params = new object[]
            {
                logTracer.TraceId,
                $"{(DateTime.Now - logTracer.CreatedAt).TotalMilliseconds,6:0.#}",
                $"{f1,10}",
                $"{f2,10}",
                message,
                exception?.ToString(),
                callerMemberName,
                callerFilePath,
                callerLineNumber
            };

            if (exception == null)
            {
                logger.Write((LogEventLevel) level, template, @params);
            }
            else
            {
                logger.Write((LogEventLevel) level, exception, template, @params);
            }
        }
    }
}