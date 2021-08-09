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
    }
}