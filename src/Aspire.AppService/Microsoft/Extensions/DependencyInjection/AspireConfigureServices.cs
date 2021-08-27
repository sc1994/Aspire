using System.Reflection;

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
        public static IAspireBuilder AddAspire(
            this IServiceCollection services,
            Assembly applicationAssembly)
        {
            var mvcBuilder = services.AddControllers();
            mvcBuilder.AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            services.AddDynamicWebApi(options =>
            {
                options.AddAssemblyOptions(applicationAssembly);

                // options.ActionRouteFactory = new ServiceActionRouteFactory(); TODO 自定义路由规则
            });

            return new AspireBuilder(mvcBuilder, services);
        }
    }
}