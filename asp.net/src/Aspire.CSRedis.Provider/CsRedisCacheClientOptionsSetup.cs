// <copyright file="CsRedisCacheClientOptionsSetup.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.CSRedis.Provider
{
    using global::CSRedis;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc />
    public class CsRedisCacheClientOptionsSetup : ICacheClientOptionsSetup
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsRedisCacheClientOptionsSetup"/> class.
        /// </summary>
        /// <param name="connectionString">Connection String.</param>
        public CsRedisCacheClientOptionsSetup(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <inheritdoc />
        public void AddCacheClient(IServiceCollection service)
        {
            service.AddSingleton<IAspireCacheClient, CacheClientRealize>();
            service.AddSingleton(serviceProvider => new CSRedisClient(this.connectionString));
        }
    }
}
