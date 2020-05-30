using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Linq;
using System.Reflection;

namespace Aspire.EfCore
{
    public static class EfCoreSetup
    {
        public static IServiceCollection AddAspireEfCore(this IServiceCollection services, Assembly assembly)
        {
            var allTypes = assembly.GetTypes();

            // 数据库上下文设置
            var options = allTypes.Where(x => x.BaseType == typeof(DbContextOptions<>));
            foreach (var item in options) services.AddTransient(item);

            // 数据库上下文
            var contexts = allTypes.Where(x => x.BaseType == typeof(DbContext));
            foreach (var item in contexts) services.AddTransient(item);

            return services;
        }
    }
}
