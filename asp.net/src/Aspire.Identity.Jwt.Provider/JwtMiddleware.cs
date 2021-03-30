// <copyright file="JwtMiddleware.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity.Jwt.Provider
{
    using System.Threading.Tasks;
    using Aspire.Identities;
    using Aspire.Identity.Jwt.Provider.AppSettings;
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
        private readonly IdentityAppSetting appSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtMiddleware{TCurrentUser}"/> class.
        /// </summary>
        /// <param name="next">Next Middleware.</param>
        /// <param name="appSetting">App Setting.</param>
        public JwtMiddleware(RequestDelegate next, IOptions<IdentityAppSetting> appSetting)
        {
            this.next = next;
            this.appSetting = appSetting.Value;
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
                    var current = JwtManage.DeconstructionJwtToken<TCurrentUser>(token, this.appSetting);

                    // attach user to context on successful jwt validation
                    context.Items[AppConst.CurrentUserHttpItemKey] = current;
                }
                catch (FriendlyException ex)
                {
                    await new GlobalResponse(ex).WriteToHttpResponseAsync(context);
                    return;
                }
            }

            await this.next(context);
        }
    }
}
