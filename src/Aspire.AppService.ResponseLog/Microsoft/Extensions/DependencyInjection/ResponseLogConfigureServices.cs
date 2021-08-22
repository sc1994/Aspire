using System.Collections.Generic;

using Aspire;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     响应 日志 服务配置.
    /// </summary>
    public static class ResponseLogConfigureServices
    {
        private static bool hasBodyOption;

        /// <summary>
        ///     添加响应日志.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="hasBody">是否包含body.</param>
        /// <returns>mvc builder.</returns>
        public static IAspireBuilder AddAspireResponseLog(
            this IAspireBuilder aspireBuilder,
            bool hasBody = true)
        {
            hasBodyOption = hasBody;
            aspireBuilder.MvcBuilder.AddMvcOptions(options => { options.Filters.Add<ResponseLogFilterAttribute>(); });

            return aspireBuilder;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class ResponseLogFilterAttribute : ActionFilterAttribute
        {
            private readonly ILogger logger;

            public ResponseLogFilterAttribute(ILogger logger)
            {
                this.logger = logger;
            }

            public override void OnResultExecuted(ResultExecutedContext context)
            {
                string f1, f2;
                if (context.ActionDescriptor is ControllerActionDescriptor contextActionDescriptor)
                {
                    f1 = contextActionDescriptor.ControllerTypeInfo.Name;
                    f2 = contextActionDescriptor.MethodInfo.Name;
                }
                else
                {
                    f1 = context.ActionDescriptor.DisplayName;
                    f2 = string.Empty;
                }

                var logMsg = new Dictionary<string, object>();

                if (hasBodyOption && context.Result is ObjectResult objectResult)
                    logMsg.Add("body", objectResult.Value);

                logger.Info(logMsg.ToJsonString(), f1, f2);
                base.OnResultExecuted(context);
            }
        }
    }
}