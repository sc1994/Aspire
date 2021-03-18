// <copyright file="ResponseActionFilterAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// 响应过滤器.
    /// </summary>
    public class ResponseActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 在控制器完成后检验控制器结果.
        /// </summary>
        /// <param name="context">Context.</param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            object logMessage = default;
            if (context.Exception is null && context.Result is ObjectResult objectResult)
            {
                var result = new OkObjectResult(new GlobalResponse(ResponseCode.Ok, objectResult.Value));
                logMessage = result.Value;
                context.Result = result;
            }
            else
            {
                switch (context.Exception)
                {
                    // 鉴定异常类型
                    case FriendlyException friendlyException:
                        var friendlyExceptionResult = new OkObjectResult(new GlobalResponse(friendlyException));
                        logMessage = friendlyExceptionResult.Value;
                        context.Result = friendlyExceptionResult;
                        break;
                    case { } exception:
                        var exceptionResult = new OkObjectResult(new GlobalResponse(exception));
                        logMessage = exceptionResult.Value;
                        context.Result = exceptionResult;
                        break;
                }

                // 删除异常让控制器正常返回
                context.Exception = null;
            }

            var logWriter = ServiceLocator.ServiceProvider.GetService<ILogWriter>();
            logWriter.Information("Response Action Executed", logMessage);

            base.OnActionExecuted(context);
        }
    }
}
