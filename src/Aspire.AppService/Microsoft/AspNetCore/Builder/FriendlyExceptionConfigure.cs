using Aspire;
using Aspire.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

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
                    ExceptionLog(cxt, friendlyException);
                    cxt.Response.ContentType = "application/json;charset=UTF-8";
                    await cxt.Response.WriteAsync(new
                    {
                        success = false,
                        title = friendlyException.Title,
                        message = friendlyException.Messages,
#if DEBUG
                        stackTrace = friendlyException.StackTrace
#endif
                    }.ToJsonString());
                }
                catch (Exception exception)
                {
                    ExceptionLog(cxt, exception);
                    cxt.Response.ContentType = "application/json;charset=UTF-8";
                    await cxt.Response.WriteAsync(new
                    {
                        success = false,
                        title = "系统异常, 请稍后重试",
                        message = exception.Message,
#if DEBUG
                        stackTrace = exception.StackTrace
#endif
                    }.ToJsonString());
                }
            });
            return app;
        }

        private static void ExceptionLog(HttpContext cxt, Exception exception)
        {
            var logger = cxt.RequestServices.GetRequiredService<ILogger>();
            var controllerActionDescriptor = cxt
                .GetEndpoint()?
                .Metadata
                .GetMetadata<ControllerActionDescriptor>();

            if (controllerActionDescriptor == null) return;

            var controllerName = controllerActionDescriptor.ControllerTypeInfo.Name;
            var actionName = controllerActionDescriptor.MethodInfo.Name;

            if (exception is FriendlyException friendlyException)
            {
                var msg = string.Join("\r\n", new[]
                {
                    friendlyException.Title
                }.Concat(friendlyException.Messages.Select(x => "\t" + x))); // 拼接消息内容 , 混合标题和内容到一个文档
                logger.Warn(friendlyException, msg, controllerName, actionName, "FriendlyException");
            }
            else
            {
                logger.Error(exception, exception.Message, controllerName, actionName, "SystemException");
            }
        }
    }
}