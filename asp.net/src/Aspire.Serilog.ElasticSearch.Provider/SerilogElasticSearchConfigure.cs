// <copyright file="SerilogElasticSearchConfigure.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Aspire.Serilog.ElasticSearch.Provider
{
    using Loggers;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    /// <inheritdoc />
    public class SerilogElasticSearchConfigure : ILoggerConfigure
    {
        /// <inheritdoc />
        public IApplicationBuilder UseLogger(IApplicationBuilder app)
        {
            app.ApplicationServices
                .GetService<LogItemsStore>()
                .InitItems();
            return app;
        }
    }
}