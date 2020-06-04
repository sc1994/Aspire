using System.Reflection;

using Aspire.Utils;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AspireSetup
    {
        public static IServiceCollection AddAspireController(this IServiceCollection services, Assembly controllersAssembly)
        {
            services
                .AddControllers()
                .AddMvcOptions(configure =>
                {
                    configure.AllowEmptyInputInBodyModelBinding = false;
                    configure.Conventions.Add(new RouteTokenTransformerConvention(new CustomTransformerConvention()));
                })
                .ConfigureApplicationPartManager(setupAction =>
                {
                    setupAction.ApplicationParts.Add(new AssemblyPart(controllersAssembly));
                })
                .AddControllersAsServices()
                .AddNewtonsoftJson();

            return services;
        }
    }
}
