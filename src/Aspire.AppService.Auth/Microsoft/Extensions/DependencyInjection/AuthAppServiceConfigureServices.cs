using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Aspire;
using Aspire.Domain.Account;
using Aspire.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     鉴/授权 服务配置.
    /// </summary>
    public static class AuthAppServiceConfigureServices
    {
        /// <summary>
        ///     添加 鉴/授权 的 app service.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="implementation">指定 account manage 的 实现方式.</param>
        /// <typeparam name="TAccount">账户.</typeparam>
        /// <typeparam name="TAccountManage">账户管理.</typeparam>
        /// <returns>mvc builder.</returns>
        public static IAspireBuilder AddAspireAuth<TAccount, TAccountManage>(
            this IAspireBuilder aspireBuilder,
            Func<IServiceProvider, TAccountManage> implementation)
            where TAccount : class, IAccount, new()
            where TAccountManage : class, IAccountManage<IAccount>
        {
            // 注册 manage
            aspireBuilder.ServiceCollection.AddScoped(implementation);
            aspireBuilder.ServiceCollection.AddScoped<IAccountManage<IAccount>, TAccountManage>();

            // 注册 账户
            aspireBuilder.ServiceCollection.AddScoped<TAccount>(provider =>
            {
                var httpContext = provider.GetRequiredService<IHttpContextAccessor>();

                if (httpContext.HttpContext == null)
                    return new TAccount();

                if (!httpContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var auth))
                    return new TAccount();

                var accountManage = provider.GetRequiredService<IAccountManage<TAccount>>();
                return accountManage.GetAccountByToken(auth);
            });
            aspireBuilder.ServiceCollection.AddScoped<IAccount, TAccount>();

            // 添加过滤器
            aspireBuilder.MvcBuilder.AddMvcOptions(options => { options.Filters.Add<AuthFilterAttribute>(); });

            return aspireBuilder;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class AuthFilterAttribute : ActionFilterAttribute
        {
            public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var account = context.HttpContext.RequestServices.GetRequiredService<IAccount>();

                AuthAttribute? auth = null;

                // 优先查找 action 上的特性.
                if (context.ActionDescriptor is ControllerActionDescriptor contextActionDescriptor)
                    auth = contextActionDescriptor.MethodInfo.GetCustomAttribute<AuthAttribute>(true);

                // 向上查找
                if (auth == null) auth = context.Controller.GetType().GetCustomAttribute<AuthAttribute>(true);

                // 没有配置特性 认为不需要验证角色
                if (auth == null)
                    return base.OnActionExecutionAsync(context, next);

                if (account?.AccountId == default)
                    throw new FriendlyException(FriendlyExceptionCode.AuthorizationFailure, "请登录", "授权信息不存在或授权信息失效");

                // 特性中没有配置角色 则不需要验证登陆人的角色
                if (auth.Roles?.Any() != true)
                    return base.OnActionExecutionAsync(context, next);

                // 特性中配置了角色而账户中没有角色
                if (account.Roles?.Any() != true)
                    throw new FriendlyException("权限不足", "正在访问不属于您权限的数据");

                // 如果 用户角色和配置的角色全部不匹配则认为越权访问
                if (auth.Roles.All(x => !account.Roles.Contains(x)) != true)
                    throw new FriendlyException("权限不足", "正在访问不属于您权限的数据");

                return base.OnActionExecutionAsync(context, next);
            }
        }
    }
}