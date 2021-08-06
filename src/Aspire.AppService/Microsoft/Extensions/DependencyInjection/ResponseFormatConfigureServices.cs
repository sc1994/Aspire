using System;
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
        /// <param name="mvcBuilder">来自于 AddControllers.</param>
        /// <returns>mvc builder.</returns>
        public static IMvcBuilder AddResponseFormat(IMvcBuilder mvcBuilder)
        {
            if (mvcBuilder == null) throw new ArgumentNullException(nameof(mvcBuilder));

            mvcBuilder.AddMvcOptions(options => { options.Filters.Add<ResponseFormatFilterAttribute>(); });

            return mvcBuilder;
        }

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
                        Result = result
                    });

                base.OnActionExecuted(context);
            }
        }
    }
}