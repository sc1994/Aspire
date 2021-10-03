using System;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    ///     swagger 运行配置.
    /// </summary>
    public static class SwaggerConfigure
    {
        /// <summary>
        ///     使用 aspire 的 全局友好异常.
        /// </summary>
        /// <param name="app">app.</param>
        /// <returns>current app.</returns>
        public static IApplicationBuilder UseAspireSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppDomain.CurrentDomain.FriendlyName} v1"));
            return app;
        }
    }
}