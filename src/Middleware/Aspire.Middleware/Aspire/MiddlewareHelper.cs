using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Aspire
{
    /// <summary>
    /// 中间件帮助类.
    /// </summary>
    public static class MiddlewareHelper
    {
        /// <summary>
        /// 根据 <see cref="HttpContext"/> 获取 Controller 和 Action 的名称.
        /// </summary>
        /// <param name="httpContext"><see cref="HttpContext"/>.</param>
        /// <returns>Controller 和 Action 的名称.</returns>
        public static (string controllerName, string actionName) GetControllerAndActionName(
            this HttpContext httpContext)
        {
            var controllerActionDescriptor = httpContext
                .GetEndpoint()?
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            if (controllerActionDescriptor == null) return (string.Empty, string.Empty);

            var controllerName = controllerActionDescriptor.ControllerTypeInfo.Name;
            var actionName = controllerActionDescriptor.MethodInfo.Name;

            return (controllerName, actionName);
        }
    }
}