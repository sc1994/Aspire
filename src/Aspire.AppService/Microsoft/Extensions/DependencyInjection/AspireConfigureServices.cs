using System;
using System.Linq;
using System.Reflection;
using Aspire;
using Panda.DynamicWebApi;

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
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAspire(
            this IServiceCollection services,
            Assembly applicationAssembly)
        {
            var setupDomain = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .First(x => x.ManifestModule.Name == AppDomain.CurrentDomain.FriendlyName + ".dll");
            
            

            services.AddDynamicWebApi(options => { options.AddAssemblyOptions(applicationAssembly); });

            services.AddScoped<IlogTracer, DefaultLogTracer>();
            services.AddScoped<ILogger, DefaultLogger>();

            return services;
        }
    }
}