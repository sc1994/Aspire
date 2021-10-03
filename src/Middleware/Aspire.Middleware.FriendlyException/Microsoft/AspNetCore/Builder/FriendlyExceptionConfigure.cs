using System;
using System.Linq;
using System.Threading.Tasks;
using Aspire;
using Aspire.Exceptions;
using Aspire.Helpers;
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
        public static IApplicationBuilder UseAspireFriendlyException(this IApplicationBuilder app)
        {
            app.Use(async (cxt, next) =>
            {
                try
                {
                    await next();
                }
                catch (FriendlyException friendlyException)
                {
                    await WriteExceptionAsync(cxt, friendlyException, friendlyException.Title);
                }
                catch (Exception exception)
                {
                    await WriteExceptionAsync(cxt, exception);
                }
            });
            return app;
        }

        private static async Task WriteExceptionAsync(HttpContext cxt, Exception exception, string? title = null)
        {
            ExceptionLog(cxt, exception);
            cxt.Response.ContentType = "application/json;charset=UTF-8";
            await cxt.Response.WriteAsync(new
            {
                success = false,
                title = title ?? exception.GetType().Name,
                messages = exception.Message.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries),
#if DEBUG
                stackTrace = exception.StackTrace?.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries),
#endif
            }.ToJsonString());
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
                }.Concat(friendlyException.Message.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Select(x => "\t" + x))); // 拼接消息内容 , 混合标题和内容到一个文档
                logger.Warn(friendlyException, msg, controllerName, actionName, nameof(FriendlyExceptionCode.BusinessException));
            }
            else
            {
                logger.Error(exception, exception.Message, controllerName, actionName, nameof(FriendlyExceptionCode.SystemException));
            }
        }
    }
}