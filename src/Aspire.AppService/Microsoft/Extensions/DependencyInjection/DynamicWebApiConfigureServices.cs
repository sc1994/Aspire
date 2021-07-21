using System.Reflection;
using Panda.DynamicWebApi;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     dynamic web api 服务配置.
    /// </summary>
    public static class DynamicWebApiConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 dynamic web api.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="applicationAssembly">要使用 mapper 的类 所属的程序集.</param>
        /// <param name="apiPreFix">api的前缀.</param>
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAspireDynamicWebApi(
            this IServiceCollection services,
            Assembly applicationAssembly,
            string apiPreFix = "api")
        {
            services.AddDynamicWebApi(options => { options.AddAssemblyOptions(applicationAssembly, apiPreFix); });

            return services;
        }
    }
}