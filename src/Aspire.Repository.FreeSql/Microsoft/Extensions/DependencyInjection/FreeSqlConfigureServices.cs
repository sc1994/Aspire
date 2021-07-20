using System;
using System.Linq;
using Aspire;
using FreeSql;
using FreeSql.Aop;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     free sql 服务配置.
    /// </summary>
    public static class FreeSqlConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 free sql.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="dataType">data type.</param>
        /// <param name="connectionString">数据库链接字符串.</param>
        /// <param name="curdAfterEvent">crud 之后的事件, 每次执行完成crud之后都会触发该方法.</param>
        /// <typeparam name="TDatabase">数据库.</typeparam>
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAspireFreeSql<TDatabase>(
            this IServiceCollection services,
            DataType dataType,
            string connectionString,
            Action<object, CurdAfterEventArgs> curdAfterEvent)
        {
            var freeSql = new FreeSqlBuilder()
                .UseConnectionString(dataType, connectionString)
#if DEBUG
                .UseAutoSyncStructure(true) // automatically synchronize the entity structure to the database
#endif
                .Build<TDatabase>();

            if (curdAfterEvent != null)
            {
                freeSql.Aop.CurdAfter += (sender, args) => { curdAfterEvent(sender, args); };
            }

            services.AddSingleton(freeSql);
            return services;
        }
    }
}