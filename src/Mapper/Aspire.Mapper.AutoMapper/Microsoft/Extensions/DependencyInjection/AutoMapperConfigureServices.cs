using System.Linq;
using System.Reflection;
using Aspire;
using Aspire.AutoMapper;
using Aspire.Helpers;
using AutoMapper;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     auto mapper 服务配置.
    /// </summary>
    public static class AutoMapperConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 auto mapper.
        /// </summary>
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="applicationAssembly">要使用 mapper 的类 所属的程序集.</param>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspireAutoMapper(
            this IAspireBuilder aspireBuilder,
            Assembly applicationAssembly)
        {
            aspireBuilder.ServiceCollection.AddSingleton(_ => // 创建 auto mapper 的实例.
            {
                return new MapperConfiguration(cfg =>
                {
                    applicationAssembly
                        .GetReferencedAssemblies()
                        .Append(applicationAssembly.GetName())
                        .SelectMany(x => Assembly.Load(x).GetTypes())
                        .ForEach(type =>
                        {
                            var mapperCaseList = type.GetCustomAttributes<MapToAttribute>();
                            if (mapperCaseList?.Any() != true) return;

                            foreach (var mapperCase in mapperCaseList)
                            {
                                cfg.CreateProfile(
                                    $"{type.FullName}_mutually_{mapperCase.MapToType.FullName}",
                                    profileConfig => { profileConfig.CreateMap(type, mapperCase.MapToType).ReverseMap(); });
                            }
                        });
                    cfg.AddMaps(applicationAssembly);
                }).CreateMapper();
            });

            aspireBuilder.ServiceCollection.AddScoped<IAspireMapper, AspireAutoMapper>();

            return aspireBuilder;
        }
    }
}