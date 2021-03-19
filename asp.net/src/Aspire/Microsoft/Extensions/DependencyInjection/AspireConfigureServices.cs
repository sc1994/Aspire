// <copyright file="AspireConfigureServices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Aspire;
    using Aspire.Identity;
    using Aspire.Logger;
    using Aspire.Mapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;
    using Panda.DynamicWebApi;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// aspire 启动.
    /// </summary>
    public static class AspireConfigureServices
    {
        /// <summary>
        /// add aspire.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <param name="dynamicWebApiOptionsSetup">Dynamic Api OptionsSetup.</param>
        /// <param name="swaggerGenOptionsSetup">Swagger OptionsSetup.</param>
        /// <param name="mapperOptions">Mapper OptionsSetup.</param>
        /// <param name="auditRepositoryOptions">Audit Repository OptionsSetup.</param>
        /// <param name="identityOptionsSetup">Identity OptionsSetup.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="newtonsoftJsonOptionsSetup">NewtonsoftJson OptionsSetup.</param>
        /// <param name="cacheClientOptionsSetup">Cache Client OptionsSetup.</param>
        /// <param name="loggerOptionsSetup">Logger OptionsSetup.</param>
        /// <returns>Service Collection .</returns>
        public static IServiceCollection AddAspire(
            this IServiceCollection services,
            Action<DynamicWebApiOptions> dynamicWebApiOptionsSetup,
            Action<SwaggerGenOptions> swaggerGenOptionsSetup,
            IAspireMapperOptionsSetup mapperOptions,
            IAuditRepositoryOptionsSetup auditRepositoryOptions,
            IIdentityOptionsSetup identityOptionsSetup,
            IConfiguration configuration,
            Action<MvcNewtonsoftJsonOptions> newtonsoftJsonOptionsSetup = null,
            ICacheClientOptionsSetup cacheClientOptionsSetup = null,
            ILoggerOptionsSetup loggerOptionsSetup = null)
        {
            return AddAspire(services, setupAction =>
            {
                setupAction.Configuration = configuration;
                setupAction.SwaggerGenOptionsSetup = swaggerGenOptionsSetup;
                setupAction.DynamicWebApiOptionsSetup = dynamicWebApiOptionsSetup;
                setupAction.AuditRepositoryOptions = auditRepositoryOptions;
                setupAction.MapperOptions = mapperOptions;
                setupAction.NewtonsoftJsonOptionsSetup = newtonsoftJsonOptionsSetup;
                setupAction.CacheClientOptionsSetup = cacheClientOptionsSetup;
                setupAction.LoggerOptionsSetup = loggerOptionsSetup;
                setupAction.IdentityOptionsSetup = identityOptionsSetup;
            });
        }

        /// <summary>
        /// add aspire.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <param name="setupAction">Setup Action.</param>
        /// <exception cref="ArgumentNullException">请注意 [NotNull] 标识.</exception>
        /// <returns>Service Collection .</returns>
        public static IServiceCollection AddAspire(
            this IServiceCollection services,
            Action<AspireSetupOptions> setupAction)
        {
            var options = new AspireSetupOptions();
            setupAction(options);

            // di服务代理 旨在以一个静态类获取 di中内容
            services
                .AddHttpContextAccessor()
                .AddSingleton<IServiceProviderProxy, HttpContextServiceProviderProxy>();

            var mvcBuilder = services.AddControllers();

            // identity
            if (options.IdentityOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.IdentityOptionsSetup));
            }

            options.IdentityOptionsSetup.AddIdentity(services);

            // NewtonsoftJson
            if (options.NewtonsoftJsonOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.NewtonsoftJsonOptionsSetup));
            }

            mvcBuilder.AddNewtonsoftJson(options.NewtonsoftJsonOptionsSetup);

            // swagger
            if (options.SwaggerGenOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.SwaggerGenOptionsSetup));
            }

            _ = services.AddSwaggerGen(x =>
            {
                options.SwaggerGenOptionsSetup(x);

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "set header: Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });

                // 一定要返回true！这是 Panda.DynamicWebApi 的限制
                x.DocInclusionPredicate((docName, description) => true);
            });

            // mapper
            if (options.MapperOptions is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.MapperOptions));
            }

            options.MapperOptions.AddAspireMapper(services);

            // audit repository
            if (options.AuditRepositoryOptions is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.AuditRepositoryOptions));
            }

            options.AuditRepositoryOptions.AddAuditRepository(services);

            // aspire configure options
            if (options.Configuration is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.Configuration));
            }

            services.Configure<AspireAppSettings>(options.Configuration.GetSection("Aspire"));

            // 引入 Panda.DynamicWebApi 自定义配置
            if (options.DynamicWebApiOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.DynamicWebApiOptionsSetup));
            }

            _ = services.AddDynamicWebApi(optionsAction =>
            {
                options.DynamicWebApiOptionsSetup(optionsAction);
            });

            // LOG
            if (options.LoggerOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.LoggerOptionsSetup));
            }

            services.AddScoped<LogWriterHelper>();
            options.LoggerOptionsSetup.AddLogger(services, options.Configuration);

            // Cache
            if (options.CacheClientOptionsSetup is null)
            {
                throw new NoNullAllowedException(nameof(AspireSetupOptions) + "." + nameof(AspireSetupOptions.CacheClientOptionsSetup));
            }

            options.CacheClientOptionsSetup.AddCacheClient(services);

            return services;
        }
    }
}
