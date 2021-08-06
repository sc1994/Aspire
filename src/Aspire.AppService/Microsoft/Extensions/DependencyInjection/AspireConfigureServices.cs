using System;
using System.Reflection;
using Aspire;
using Microsoft.Extensions.Logging;
using Panda.DynamicWebApi;
using ILogger = Aspire.ILogger;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     aspire 服务配置.
    /// </summary>
    public static class AspireConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 dynamic web api.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="applicationAssembly">要使用 mapper 的类 所属的程序集.</param>
        /// <param name="apiPreFix">api的前缀.</param>
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAspire(
            this IServiceCollection services,
            Assembly applicationAssembly,
            string apiPreFix = "api")
        {
            services.AddDynamicWebApi(options =>
            {
                options.AddAssemblyOptions(applicationAssembly, apiPreFix);
            });

            services.AddScoped<IlogTracer, DefaultLogTracer>();
            services.AddScoped<ILogger, DefaultLogger>();

            return services;
        }

        private class DefaultLogTracer : IlogTracer
        {
            public DefaultLogTracer()
            {
                TraceId = Guid.NewGuid().ToString();
            }

            public string TraceId { get; }
        }

        private class DefaultLogger : ILogger
        {
            private readonly ILogger<DefaultLogger> logger;
            private readonly IlogTracer logTracer;

            public DefaultLogger(ILogger<DefaultLogger> logger, IlogTracer logTracer)
            {
                this.logger = logger;
                this.logTracer = logTracer;
            }

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
                    "[traceId][callerFilePath:{0,16}][callerMemberName:{0,16}][callerLineNumber:{0,4}] [f1:{0,32}][f2:{0,32}] message exception",
                    logTracer.TraceId,
                    callerFilePath,
                    callerMemberName,
                    callerLineNumber,
                    f1,
                    f2,
                    message,
                    exception);
            }
        }
    }
}