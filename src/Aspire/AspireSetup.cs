using Aspire.Utils;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Aspire
{
    public static class AspireSetup
    {
        public static IMvcBuilder AddAspire(this IMvcBuilder mvc, Assembly controllersAssembly)
        {
            return mvc.AddMvcOptions(configure =>
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
        }
    }
}
