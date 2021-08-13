namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 鉴/授权 服务配置.
    /// </summary>
    public static class AuthAppServiceConfigureServices
    {
        /// <summary>
        /// 添加 鉴/授权 的 app service.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAuthAppService(this IServiceCollection services)
        {
            return services;
        }
    }
}