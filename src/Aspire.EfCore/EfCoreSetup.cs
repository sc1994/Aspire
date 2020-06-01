using Aspire.EfCore;

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
            var allTypes = assembly.GetTypes();


            // 上下文设置
            var builds = allTypes.Where(x => x.BaseType == typeof(DbContextOptionsBuilder<>));
            foreach (var item in builds)
            {
                var instance = Activator.CreateInstance(item);

                // 注入单例构建
                services.AddSingleton(instance);

                // 注入单例设置
                var option = item.GetProperty("Options").GetValue(instance);
                services.AddSingleton(option);
            }

            // 上下文
            var contexts = allTypes.Where(x => x.BaseType == typeof(DbContext));
            foreach (var item in contexts) services.AddTransient(item);

            return services;
        }
    }
}
