namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    ///     swagger 运行配置.
    /// </summary>
    public static class SwaggerConfigure
    {
        /// <summary>
        ///     使用 aspire 的 swagger.
        /// </summary>
        /// <param name="app">app.</param>
        /// <param name="title">swagger doc title.</param>
        /// <returns>current app.</returns>
        public static IApplicationBuilder UseAspireSwagger(
            this IApplicationBuilder app,
            string title)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{title} v1"));
            return app;
        }
    }
}