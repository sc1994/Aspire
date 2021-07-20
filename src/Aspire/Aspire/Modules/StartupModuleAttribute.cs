using System;
using Microsoft.Extensions.DependencyInjection;

namespace Aspire.Modules
{
    /// <summary>
    ///     启动模块特性.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class StartupModuleAttribute : Attribute
    {
        /// <summary>
        ///     服务配置.
        /// </summary>
        /// <param name="services">服务.</param>
        public abstract void ConfigureServices(IServiceCollection services);

        /// <summary>
        ///     配置.
        /// </summary>
        /// <param name="serviceProvider">服务提供者.</param>
        public abstract void Configure(IServiceProvider serviceProvider);
    }
}