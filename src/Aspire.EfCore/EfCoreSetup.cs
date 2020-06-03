using Aspire.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfCoreSetup
    {
        private static bool _isCanCommon = true;

        public static IServiceCollection AddAspireEfCore<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            if (_isCanCommon && !(_isCanCommon = false))
            {
                // 常规仓储注入
                services.AddScoped(typeof(IRepository<>), typeof(RepositoryEfCore<>));
                services.AddScoped(typeof(IRepository<,>), typeof(RepositoryEfCore<,>));

                services.AddScoped(typeof(Repository<>), typeof(RepositoryEfCore<>));
                services.AddScoped(typeof(Repository<,>), typeof(RepositoryEfCore<,>));

                services.AddScoped(typeof(IRepositoryEfCore<>), typeof(RepositoryEfCore<>));
                services.AddScoped(typeof(IRepositoryEfCore<,>), typeof(RepositoryEfCore<,>));
            }

            var allTypes = typeof(TDbContext).Assembly.GetTypes();

            // 设置构建项
            var build = allTypes.FirstOrDefault(x => x.BaseType == typeof(DbContextOptionsBuilder<TDbContext>));
            var instance = Activator.CreateInstance(build);

            // 注入单例 DbContextOptionsBuilder
            services.AddSingleton(instance);

            // 注入单例 DbContextOptions
            var option = build.GetProperties().First(x => x.Name == "Options").GetValue(instance);
            services.AddSingleton(typeof(DbContextOptions<TDbContext>), option);

            // 上下文
            var contexts = allTypes.Where(x => x.BaseType == typeof(DbContext));
            foreach (var item in contexts) services.AddScoped(typeof(DbContext), item);

            return services;
        }
    }
}
