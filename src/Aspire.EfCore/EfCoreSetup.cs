using Aspire.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfCoreSetup
    {
        public static IServiceCollection AddAspireEfCore(this IServiceCollection services, Assembly assembly)
        {
            // 常规仓储注入
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryEfCore<>));
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryEfCore<,>));

            services.AddScoped(typeof(Repository<>), typeof(RepositoryEfCore<>));
            services.AddScoped(typeof(Repository<,>), typeof(RepositoryEfCore<,>));

            services.AddScoped(typeof(IRepositoryEfCore<>), typeof(RepositoryEfCore<>));
            services.AddScoped(typeof(IRepositoryEfCore<,>), typeof(RepositoryEfCore<,>));

            var allTypes = assembly.GetTypes();

            // 上下文设置
            var builds = allTypes.Where(x => x.BaseType.BaseType == typeof(DbContextOptionsBuilder));
            foreach (var item in builds)
            {
                var instance = Activator.CreateInstance(item);

                // 注入单例 DbContextOptionsBuilder
                services.AddSingleton(instance);

                // 注入单例 DbContextOptions
                var option = item.GetProperties().First(x => x.Name == "Options").GetValue(instance);
                services.AddSingleton(typeof(DbContextOptions), option);
            }

            // 上下文
            var contexts = allTypes.Where(x => x.BaseType == typeof(DbContext));
            foreach (var item in contexts) services.AddScoped(typeof(DbContext), item);

            return services;
        }
    }
}
