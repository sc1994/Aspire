// <copyright file="AspireStartupConfigure.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    using System;
    using System.Data;
    using Aspire;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// aspire 启动.
    /// </summary>
    public static class AspireStartupConfigure
    {
        /// <summary>
        /// 使用 aspire.
        /// </summary>
        /// <param name="app">Application Builder.</param>
        /// <param name="actionConfigure">请注意 [NotNull] 标识.</param>
        /// <returns>Application Builder .</returns>
        public static IApplicationBuilder UseAspire(
            this IApplicationBuilder app,
            Action<AspireUseConfigure> actionConfigure)
        {
            var configure = new AspireUseConfigure();
            actionConfigure(configure);

            if (configure.EndpointRouteConfigure is null)
            {
                throw new NoNullAllowedException(nameof(AspireUseConfigure) + "." + nameof(AspireUseConfigure.EndpointRouteConfigure));
            }

            if (configure.SwaggerUiName.IsNullOrWhiteSpace())
            {
                throw new NoNullAllowedException(nameof(AspireUseConfigure) + "." + nameof(AspireUseConfigure.SwaggerUiName));
            }

            configure.LoggerConfigure?.UseLogger(app);

            // 初始化 di服务代理 到 静态服务定位类中
            ServiceLocator.Initialize(app.ApplicationServices.GetService<IServiceProviderProxy>());

            // 启用 swagger ui
            var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", configure.SwaggerUiName));
            }

            app.UseRouting();

            app.UseEndpoints(configure.EndpointRouteConfigure);

            return app;
        }
    }
}