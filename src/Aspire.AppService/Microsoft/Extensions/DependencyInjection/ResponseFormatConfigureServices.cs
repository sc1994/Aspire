using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     响应 格式化 服务配置.
    /// </summary>
    public static class ResponseFormatConfigureServices
    {
        /// <summary>
        ///     添加响应格式化.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <returns>mvc builder.</returns>
        public static IAspireBuilder AddAspireResponseFormat(this IAspireBuilder aspireBuilder)
        {
            if (aspireBuilder == null) throw new ArgumentNullException(nameof(aspireBuilder));

            aspireBuilder.MvcBuilder.AddMvcOptions(options =>
            {
                options.Filters.Add<ResponseFormatFilterAttribute>();
            });

            return aspireBuilder;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class ResponseFormatFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext context)
            {
                if (context == null) throw new ArgumentNullException(nameof(context));
                if (context.Exception != null) return;

                if (context.Result is ObjectResult result)
                    context.Result = new ObjectResult(new
                    {
                        success = true,
                        Result = result.Value
                    });

                base.OnActionExecuted(context);
            }
        }
    }
}