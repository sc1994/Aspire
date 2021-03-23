// <copyright file="AuthenticationAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Aspire.Identity;
    using Aspire.Logger;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Authentication.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationAttribute"/> class.
        /// </summary>
        public AuthenticationAttribute()
        {
            this.CurrentRoles = Array.Empty<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationAttribute"/> class.
        /// </summary>
        /// <param name="roles">角色集合以,分割.</param>
        public AuthenticationAttribute(string roles)
        {
            this.CurrentRoles = roles
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }

        /// <summary>
        /// Gets or sets 角色.
        /// </summary>
        public string[] CurrentRoles { get; set; }

        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var filters = context.ActionDescriptor.FilterDescriptors.OrderBy(x => x.Order);

            if (filters.All(x => x.Filter is not IgnoreLogAttribute))
            {
                var logWriter = ServiceLocator.ServiceProvider.GetService<ILogWriter>();
                logWriter.Information("Request Executing", context.ActionArguments);
            }

            if (filters.Any(x => x.Filter is IgnoreAuthenticationAttribute))
            {
                base.OnActionExecuting(context);
                return;
            }

            // 没有鉴权标识
            if (!(filters.FirstOrDefault(x => x.Filter is AuthenticationAttribute)?.Filter is AuthenticationAttribute authorize))
            {
                base.OnActionExecuting(context);
                return;
            }

            // 兼容 Allow Anonymous
            if (HasAllowAnonymous(context.ActionDescriptor))
            {
                base.OnActionExecuting(context);
                return;
            }

            // 类型错误(未登录)
            if (!(context.HttpContext.Items[AppConst.CurrentUserHttpItemKey] is ICurrentUser user))
            {
                var tmp = FriendlyThrowException.ThrowException(ResponseCode.Unauthorized, "当前操作需要登入");
                context.Result = new JsonResult(new GlobalResponse(tmp))
                {
                    StatusCode = 200,
                };
                return;
            }

            // 用户是admin
            if (user.Roles.Contains(Roles.Admin))
            {
                base.OnActionExecuting(context);
                return;
            }

            // 配置了指定角色
            if (authorize.CurrentRoles.Any())
            {
                // 角色不包含在指定角色中
                if (user.Roles.Any()
                    || user.Roles.All(x => !authorize.CurrentRoles.Contains(x)))
                {
                    var tmp = FriendlyThrowException.ThrowException(ResponseCode.UnauthorizedRoles, "当前用户权限不足");
                    context.Result = new JsonResult(new GlobalResponse(tmp))
                    {
                        StatusCode = 200,
                    };
                }
            }
        }

        private static bool HasAllowAnonymous(ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor contextActionDescriptor)
            {
                if (contextActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                {
                    return true;
                }

                if (contextActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
