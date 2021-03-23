// <copyright file="IIdentityOptionsSetup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Identity
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// 身份 启动项.
    /// </summary>
    public interface IIdentityOptionsSetup
    {
        /// <summary>
        /// 添加身份.
        /// </summary>
        /// <param name="service">Service Collection.</param>
        void AddIdentity(IServiceCollection service);
    }
}
