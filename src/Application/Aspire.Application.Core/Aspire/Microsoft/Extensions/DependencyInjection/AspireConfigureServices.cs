using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;

using Panda.DynamicWebApi;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     aspire 服务配置.
    /// </summary>
    public static class AspireConfigureServices
    {
        private static string? assemblyName;

        /// <summary>
        /// Gets 程序集名称(作为swagger 的 title).
        /// </summary>
        internal static string? AssemblyName { get => assemblyName; private set => assemblyName = value; }

        /// <summary>
        ///     添加 aspire 的 dynamic web api.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="applicationAssembly">要使用 mapper 的类 所属的程序集.</param>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspire(
            this IServiceCollection services,
            Assembly applicationAssembly)
        {
            var mvcBuilder = services.AddControllers();
            mvcBuilder.AddNewtonsoftJson();

            services.AddHttpContextAccessor();

            services.AddDynamicWebApi(options =>
            {
                options.AddAssemblyOptions(applicationAssembly);

                options.ActionRouteFactory = new ServiceActionRouteFactory();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AssemblyName = AppDomain.CurrentDomain.FriendlyName,
                    Version = "v1"
                });

                // TODO 验证是否需要加小锁标记
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "设置 Authorization.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                c.OperationFilter<AuthOperationFilter>();

                c.TagActionsBy(tagSelector =>
                {
                    return new[] { GetFriendlyControllerName(tagSelector.GroupName ?? "Default") };
                });

                c.DocInclusionPredicate((_, _) => true);
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);
            });

            return new AspireBuilder(mvcBuilder, services);
        }

        private static string GetFriendlyControllerName(string controllerName)
        {
            if (controllerName != "Application")
            {
                return controllerName.Replace("Application", string.Empty);
            }

            return controllerName;
        }

        private class AuthOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                // TODO
                // https://stackoverflow.com/questions/56745739/in-swagger-ui-how-can-i-remove-the-padlock-icon-from-anonymous-methods
            }
        }

        private class ServiceActionRouteFactory : IActionRouteFactory
        {
            public string CreateActionRouteModel(string areaName, string controllerName, ActionModel action)
            {
                var actionName = action.ActionName;
                if ((action.ActionMethod.Name == "CreateOrUpdateAsync"
                    || action.ActionMethod.Name == "CreateOrUpdate")
                    && actionName == "OrUpdate")
                {
                    actionName = "CreateOrUpdate";
                }

                return Path
                    .Combine(
                        GetApiPreFix(action),
                        areaName ?? string.Empty,
                        GetFriendlyControllerName(controllerName),
                        actionName)
                    .Replace("\\", "/");
            }

            private static string GetApiPreFix(ActionModel action)
            {
                if (AppConsts.AssemblyDynamicWebApiOptions.TryGetValue(action.Controller.ControllerType.Assembly, out var value)
                    && !string.IsNullOrWhiteSpace(value?.ApiPrefix))
                {
                    return value.ApiPrefix;
                }

                return AppConsts.DefaultApiPreFix;
            }
        }
    }
}