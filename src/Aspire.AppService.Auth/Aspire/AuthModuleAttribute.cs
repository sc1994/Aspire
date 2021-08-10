using System;
using Aspire.Modules;

namespace Aspire
{
    /// <summary>
    /// 认证模块.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthModuleAttribute : ModuleAttribute
    {
        /// <inheritdoc />
        public override void ModuleConfigureServices(IServiceProvider serviceProvider)
        {
        }
    }
}