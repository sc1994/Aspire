using Aspire;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     请求日志 服务配置.
    /// </summary>
    public static class RequestLogConfigureServices
    {
        private static Func<KeyValuePair<string, StringValues>, bool>? headerKeyFilterOption;
        private static bool hasBodyOption;

        /// <summary>
        ///     添加请求日志.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="headerKeyFilter">请求 header 的过滤.</param>
        /// <param name="hasBody">是否包含body.</param>
        /// <returns>mvc builder.</returns>
        public static IAspireBuilder AddAspireRequestLog(
            this IAspireBuilder aspireBuilder,
            Func<KeyValuePair<string, StringValues>, bool>? headerKeyFilter = null,
            bool hasBody = true)
        {
            hasBodyOption = hasBody;
            headerKeyFilterOption = headerKeyFilter;
            aspireBuilder.MvcBuilder.AddMvcOptions(options => { options.Filters.Add<RequestLogFilterAttribute>(); });

            return aspireBuilder;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
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
                    f1 = contextActionDescriptor.ControllerTypeInfo.Name;
                    f2 = contextActionDescriptor.MethodInfo.Name;
                }
                else
                {
                    f1 = context.ActionDescriptor.DisplayName ?? string.Empty;
                    f2 = string.Empty;
                }

                var logMsg = new Dictionary<string, object>
                {
                    {
                        "method", context.HttpContext.Request.Method
                    },
                    {
                        "path", context.HttpContext.Request.Path
                    },
                };

                var headers = context
                    .HttpContext
                    .Request
                    .Headers
#pragma warning disable CS8604 // Possible null reference argument.
                    .WhereIf(headerKeyFilterOption != null, headerKeyFilterOption)
#pragma warning restore CS8604 // Possible null reference argument.
                    .ToDictionary(x => x.Key, x => x.Value);
                if (headers.Any()) logMsg.Add("headers", headers);

                if (hasBodyOption)
                    logMsg.Add("params", context
                        .ActionArguments
                        .ToDictionary(x => x.Key, x => x.Value));

                logger.Info(
                    logMsg.ToJsonString(),
                    f1,
                    f2,
                    "RequestLog");
                return base.OnActionExecutionAsync(context, next);
            }
        }
    }
}