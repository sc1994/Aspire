using System.Linq;
using System.Threading.Tasks;
using Aspire;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 请求日志 服务配置.
    /// </summary>
    public static class RequestLogConfigureServices
    {
        /// <summary>
        /// 添加请求日志.
        /// </summary>
        /// <param name="mvcBuilder">来自于 AddControllers.</param>
        /// <returns>mvc builder.</returns>
        public static IMvcBuilder AddRequestLog(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(options => { options.Filters.Add<RequestLogFilterAttribute>(); });
        }

        private class RequestLogFilterAttribute : ActionFilterAttribute
        {
            private readonly ILogger logger;

            public RequestLogFilterAttribute(ILogger logger)
            {
                this.logger = logger;
            }

            public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                string f1, f2;

                if (context.ActionDescriptor is ControllerActionDescriptor contextActionDescriptor)
                {
                    f1 = contextActionDescriptor.ControllerName;
                    f2 = contextActionDescriptor.ActionName;
                }
                else
                {
                    f1 = context.ActionDescriptor.DisplayName;
                    f2 = string.Empty;
                }

                logger.Info(
                    new
                    {
                        context.HttpContext.Request.Method,
                        context.HttpContext.Request.Path,
                        Headers = context.HttpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value),
                        Body = context.ActionArguments.ToDictionary(x => x.Key, x => x.Value)
                    }.ToJsonString(),
                    f1,
                    f2);
                return base.OnActionExecutionAsync(context, next);
            }
        }
    }
}