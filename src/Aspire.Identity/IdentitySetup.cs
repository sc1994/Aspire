using System;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Aspire.Identity
{
    public static class IdentitySetup
    {
        public static IServiceCollection AddAspireIdentity<TIdentityDbContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TIdentityDbContext : IdentityDbContext
            => services.AddAspireIdentity<TIdentityDbContext, IdentityUser>(configuration, optionsAction, contextLifetime, optionsLifetime);

        public static IServiceCollection AddAspireIdentity<TIdentityDbContext, TIdentityUser>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TIdentityDbContext : IdentityDbContext
            where TIdentityUser : IdentityUser
            => services.AddAspireIdentity<TIdentityDbContext, IdentityUser, IdentityRole>(configuration, optionsAction, contextLifetime, optionsLifetime);

        public static IServiceCollection AddAspireIdentity<TIdentityDbContext, TIdentityUser, TIdentityRole>(
            this IServiceCollection services,
            IConfiguration configuration,
            Action<DbContextOptionsBuilder> optionsAction,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
            where TIdentityDbContext : IdentityDbContext
            where TIdentityUser : IdentityUser
            where TIdentityRole : IdentityRole
        {
            services.AddDbContext<TIdentityDbContext>(optionsAction, contextLifetime, optionsLifetime);

            services.AddDefaultIdentity<TIdentityUser>()
                .AddRoles<TIdentityRole>()
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();

            var appSettingsSection = configuration.GetSection("IdentitySettings");
            services.Configure<IdentitySettings>(appSettingsSection);

            var settings = appSettingsSection.Get<IdentitySettings>();
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            var authentication = services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            authentication.AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = settings.ValidAt,
                    ValidIssuer = settings.Issuer
                };
            });

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanWriteCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Write")));
                options.AddPolicy("CanRemoveCustomerData", policy => policy.Requirements.Add(new ClaimRequirement("Customers", "Remove")));
            });

            return services;
        }

        public static IApplicationBuilder UseAspireIdentity(this IApplicationBuilder app)
        {
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                // 登入
                
            });

            return app;
        }
    }
}
