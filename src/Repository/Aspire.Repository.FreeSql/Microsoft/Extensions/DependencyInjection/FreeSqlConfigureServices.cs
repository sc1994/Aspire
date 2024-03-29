﻿using System;
using Aspire;
using Aspire.Repository.FreeSql;
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
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="dataType">data type.</param>
        /// <param name="connectionString">数据库链接字符串.</param>
        /// <param name="curdAfterEvent">crud 之后的事件, 每次执行完成crud之后都会触发该方法.</param>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspireFreeSql(
            this IAspireBuilder aspireBuilder,
            DataType dataType,
            string connectionString,
            Action<IServiceProvider, object, CurdAfterEventArgs>? curdAfterEvent = null)
        {
            aspireBuilder.ServiceCollection.AddSingleton(provider =>
                GetFreeSql(dataType, connectionString, provider, curdAfterEvent));
            aspireBuilder.ServiceCollection.AddScoped(typeof(IRepository<,,>), typeof(RepositoryFreeSql<,,>));
            aspireBuilder.ServiceCollection.AddScoped(typeof(IRepository<,>), typeof(RepositoryFreeSql<,>));
            aspireBuilder.ServiceCollection.AddScoped(typeof(IRepository<>), typeof(RepositoryFreeSql<>));

            return aspireBuilder; // TODO 重复代码
        }

        /// <summary>
        ///     添加 aspire 的 free sql.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="dataType">data type.</param>
        /// <param name="connectionString">数据库链接字符串.</param>
        /// <param name="curdAfterEvent">crud 之后的事件, 每次执行完成crud之后都会触发该方法.</param>
        /// <typeparam name="TDatabase">数据库.</typeparam>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspireFreeSql<TDatabase>(
            this IAspireBuilder aspireBuilder,
            DataType dataType,
            string connectionString,
            Action<IServiceProvider, object, CurdAfterEventArgs>? curdAfterEvent = null)
        {
            aspireBuilder.ServiceCollection.AddSingleton(provider =>
                GetFreeSql<TDatabase>(dataType, connectionString, provider, curdAfterEvent));
            aspireBuilder.ServiceCollection.AddScoped(typeof(IRepository<,,>), typeof(RepositoryFreeSql<,,>));
            return aspireBuilder;
        }

        private static IFreeSql GetFreeSql(
            DataType dataType,
            string connectionString,
            IServiceProvider provider,
            Action<IServiceProvider, object, CurdAfterEventArgs>? curdAfterEvent = null)
        {
            var freeSql = new FreeSqlBuilder()
                .UseConnectionString(dataType, connectionString)
#if DEBUG
                .UseAutoSyncStructure(true) // automatically synchronize the entity structure to the database
#endif
                .Build();

            if (curdAfterEvent != null)
            {
                freeSql.Aop.CurdAfter += (sender, args) =>
                {
                    if (sender == null)
                        throw new ArgumentNullException(nameof(sender));

                    curdAfterEvent(provider, sender, args);
                };
            }

            return freeSql;
        }

        private static IFreeSql<TDatabase> GetFreeSql<TDatabase>(
            DataType dataType,
            string connectionString,
            IServiceProvider provider,
            Action<IServiceProvider, object, CurdAfterEventArgs>? curdAfterEvent = null)
        {
            var freeSql = new FreeSqlBuilder()
                .UseConnectionString(dataType, connectionString)
#if DEBUG
                .UseAutoSyncStructure(true) // automatically synchronize the entity structure to the database
#endif
                .Build<TDatabase>();

            if (curdAfterEvent != null)
            {
                freeSql.Aop.CurdAfter += (sender, args) =>
                {
                    if (sender == null)
                        throw new ArgumentNullException(nameof(sender));

                    curdAfterEvent(provider, sender, args);
                };
            }

            return freeSql;
        }
    }
}