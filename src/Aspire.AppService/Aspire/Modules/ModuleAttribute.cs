using System;

namespace Aspire.Modules
{
    /// <summary>
    ///     启动模块特性.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class ModuleAttribute : Attribute
    {
        /// <summary>
        /// 配置模块服务.
        /// </summary>
        /// <param name="serviceProvider">di中的内容.</param>
        public abstract void ModuleConfigureServices(IServiceProvider serviceProvider);
    }
}