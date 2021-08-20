using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        /// <param name="aspireBuilder">服务.</param>
        /// <param name="title">swagger doc title.</param>
        /// <returns>当前服务.</returns>
        public static IAspireBuilder AddAspireSwagger(
            this IAspireBuilder aspireBuilder,
            string title)
        {
            aspireBuilder.ServiceCollection.AddSwaggerGen(c =>
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

                c.DocInclusionPredicate((_, _) => true);
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var xmlFile = AppDomain.CurrentDomain.FriendlyName + ".xml";
                var xmlPath = Path.Combine(baseDirectory, xmlFile);
                if (File.Exists(xmlPath)) c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AuthOperationFilter>();
            });
            return aspireBuilder;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class AuthOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                // TODO
                // https://stackoverflow.com/questions/56745739/in-swagger-ui-how-can-i-remove-the-padlock-icon-from-anonymous-methods
            }
        }
    }
}