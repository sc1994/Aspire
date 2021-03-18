// <copyright file="AuthorizationFilterAttribute.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Authorization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFilterAttribute"/> class.
        /// </summary>
        public AuthorizationFilterAttribute()
        {
            this.CurrentRoles = Array.Empty<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizationFilterAttribute"/> class.
        /// </summary>
        /// <param name="roles">角色集合以,分割.</param>
        public AuthorizationFilterAttribute(string roles)
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

        /// <summary>
        /// 鉴权.
        /// </summary>
        /// <param name="context">Context.</param>
        [SuppressMessage("Style", "IDE0083:使用模式匹配", Justification = "<挂起>")]
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor contextActionDescriptor))
            {
                return;
            }

            var allowAnonymous = contextActionDescriptor
                .MethodInfo
                .GetCustomAttributes<AllowAnonymousAttribute>()
                .FirstOrDefault();
            if (allowAnonymous != null)
            {
                return;
            }

            // 尝试查找鉴权特性
            var authorize = contextActionDescriptor
                .MethodInfo
                .GetCustomAttributes<AuthorizationFilterAttribute>()
                .FirstOrDefault() ?? contextActionDescriptor
                .ControllerTypeInfo
                .GetCustomAttributes<AuthorizationFilterAttribute>()
                .FirstOrDefault();

            // 没有鉴权标识
            if (authorize is null)
            {
                return;
            }

            // 类型错误(未登录)
            if (!(context.HttpContext.Items[AppConst.CurrentUserHttpItemKey] is ICurrentUser user))
            {
                var tmpResponse = new GlobalResponse(FriendlyThrowException.ThrowException(
                    ResponseCode.Unauthorized, "当前操作需要登入"));
                context.Result = new JsonResult(tmpResponse)
                {
                    StatusCode = StatusCodes.Status200OK,
                };
                return;
            }

            // 用户是admin
            if (user.Roles == Roles.Admin)
            {
                return;
            }

            // 配置了指定角色
            if (authorize.CurrentRoles.Any())
            {
                // 角色不包含在指定角色中
                if (user.Roles.IsNullOrWhiteSpace()
                    || user.Roles
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .All(x => !authorize.CurrentRoles.Contains(x)))
                {
                    context.Result = new JsonResult(new GlobalResponse(FriendlyThrowException.ThrowException(ResponseCode.UnauthorizedRoles, "当前用户权限不足")))
                    {
                        StatusCode = StatusCodes.Status200OK,
                    };
                }
            }
        }
    }
}
