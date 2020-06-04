using System.Reflection;

using Aspire.Application.AppServices.Dtos;
using Aspire.AutoMapper;
using Aspire.Map;

using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddAspireAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembly);

            }).CreateMapper();

            return services.AddSingleton<IAspireMapper>(new AspireMapper(mapper)); // 注入 map 实例
        }
    }
}
