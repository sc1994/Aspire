// <copyright file="ICacheClientOptionsSetup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Aspire Redis Options Setup.
    /// </summary>
    public interface ICacheClientOptionsSetup
    {
        /// <summary>
        /// Add Cache Client.
        /// </summary>
        /// <param name="service">Service Collection.</param>
        void AddCacheClient(IServiceCollection service);
    }
}
