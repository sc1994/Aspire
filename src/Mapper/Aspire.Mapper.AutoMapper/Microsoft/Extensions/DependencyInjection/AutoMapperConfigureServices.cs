using System.Reflection;
using Aspire;
using Aspire.AutoMapper;
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
                    cfg.AddMaps(applicationAssembly);
                    applicationAssembly
                        .GetTypes()
                        .ForEach(type =>
                        {
                            var mapperCase = type.GetCustomAttribute<MapToAttribute>();
                            if (mapperCase is null) return;

                            cfg.CreateProfile(
                                $"{type.FullName}_mutually_{mapperCase.MapToType.FullName}",
                                profileConfig => { profileConfig.CreateMap(type, mapperCase.MapToType).ReverseMap(); });
                        });
                }).CreateMapper();
            });

            aspireBuilder.ServiceCollection.AddScoped<IAspireMapper, AspireAutoMapper>();

            return aspireBuilder;
        }
    }
}