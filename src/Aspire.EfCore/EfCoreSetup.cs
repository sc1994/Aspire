using System.Linq;
using System.Reflection;

using Aspire.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EfCoreSetup
    {
        private static bool _isCanCommon = true;

        public static IServiceCollection AddAspireEfCore(this IServiceCollection services, Assembly dbAssembly)
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

            // 上下文
            var contexts = dbAssembly.GetTypes().Where(x => x.BaseType == typeof(DbContext));
            foreach (var item in contexts) services.AddScoped(typeof(DbContext), item);

            return services;
        }
    }
}
