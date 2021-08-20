using System;
using Aspire.SeriLogger;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     serilog 服务配置.
    /// </summary>
    public static class SerilogConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 serilog.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="configuration">日志配置.</param>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspireSerilog(
            this IAspireBuilder aspireBuilder,
            Action<Serilog.LoggerConfiguration> configuration)
        {
            aspireBuilder.ServiceCollection.AddSingleton<Serilog.ILogger>(provider =>
            {
                var loggerConfiguration = new Serilog.LoggerConfiguration();
                configuration(loggerConfiguration);
                return loggerConfiguration.CreateLogger();
            });
            aspireBuilder.ServiceCollection.AddScoped<Aspire.ILogger, SeriLogger>();

            return aspireBuilder;
        }
    }
}