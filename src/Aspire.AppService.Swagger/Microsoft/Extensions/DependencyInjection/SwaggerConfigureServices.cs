using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     swagger 服务配置.
    /// </summary>
    public static class SwaggerConfigureServices
    {
        /// <summary>
        ///     添加 aspire 的 swagger.
        /// </summary>
        /// <param name="services">服务.</param>
        /// <param name="title">swagger doc title.</param>
        /// <returns>当前服务.</returns>
        public static IServiceCollection AddAspireSwagger(
            this IServiceCollection services,
            string title)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = title,
                    Version = "v1"
                });

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

                c.DocInclusionPredicate((docName, description) => true);
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory ?? string.Empty;
                var xmlFile = AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }
}