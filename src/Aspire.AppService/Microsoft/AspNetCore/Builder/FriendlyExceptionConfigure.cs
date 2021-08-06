using System;
using Aspire;
using Aspire.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    ///     友好的异常 运行配置.
    /// </summary>
    public static class FriendlyExceptionConfigure
    {
        /// <summary>
        ///     使用 aspire 的 全局友好异常.
        /// </summary>
        /// <param name="app">app.</param>
        /// <returns>current app.</returns>
        public static IApplicationBuilder UseFriendlyException(this IApplicationBuilder app)
        {
            app.Use(async (cxt, next) =>
            {
                try
                {
                    await next();
                }
                catch (FriendlyException friendlyException)
                {
                    await cxt.Response.WriteAsync(new
                    {
                        Success = false,
                        friendlyException.Title,
                        friendlyException.Messages,
#if DEBUG
                        friendlyException.StackTrace
#endif
                    }.ToJsonString());
                }
                catch (Exception exception)
                {
                    await cxt.Response.WriteAsync(new
                    {
                        Success = false,
                        Title = "系统异常, 请稍后重试",
                        exception.Message,
#if DEBUG
                        exception.StackTrace
#endif
                    }.ToJsonString());
                }
            });
            return app;
        }
    }
}