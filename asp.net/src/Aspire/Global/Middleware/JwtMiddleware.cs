// <copyright file="JwtMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Aspire.Authenticate;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Jwt Middleware.
    /// </summary>
    /// <typeparam name="TCurrentUser">Current User.</typeparam>
    internal class JwtMiddleware<TCurrentUser>
        where TCurrentUser : ICurrentUser, new()
    {
        private readonly RequestDelegate next;
        private readonly AspireAppSettings aspireSetupOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtMiddleware{TCurrentUser}"/> class.
        /// </summary>
        /// <param name="next">Next Middleware.</param>
        /// <param name="aspireAppSettings">App Settings.</param>
        public JwtMiddleware(RequestDelegate next, IOptions<AspireAppSettings> aspireAppSettings)
        {
            this.next = next;
            this.aspireSetupOptions = aspireAppSettings.Value;
        }

        /// <summary>
        /// Invoke.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <returns>Task.</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                try
                {
                    var current = new JwtManage(this.aspireSetupOptions.Jwt).DeconstructionJwtToken<TCurrentUser>(token);

                    // attach user to context on successful jwt validation
                    context.Items[AppConst.CurrentUserHttpItemKey] = current;
                }
                catch (FriendlyException ex)
                {
                    await AuthorizationExpired(context, ex);
                    return;
                }
            }

            await this.next(context);
        }

        private static async Task AuthorizationExpired(HttpContext context, FriendlyException ex)
        {
            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json; charset=utf-8";
            await context.Response.WriteAsync(
                new GlobalResponse
                {
                    Code = ex.Code,
                    Messages = ex.Messages,
                    Result = null,
#if DEBUG
                    StackTrace = ex.StackTrace,
#endif
                    Title = ex.Title,
                }.SerializeObject());
        }
    }
}
