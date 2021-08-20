using System.Linq;
using System.Reflection;
using Aspire;
using Aspire.Domain.Account;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using template_1qaz2wsx3edc.Entity.MainDatabase;

#pragma warning disable 1591

namespace template_1qaz2wsx3edc.HostApi
{
    public class Startup
    {
        private static readonly string[] logHeaderKeys =
        {
            "Content-Length"
        };

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CustomAccountManage>();

            var appServiceAssembly = Assembly.Load($"{typeof(Startup).Namespace?.Replace("HostApi", "AppService")}");

            services
                .AddAspire(appServiceAssembly)
                .AddAspireSwagger(typeof(Startup).Namespace)
                .AddAspireAutoMapper(appServiceAssembly)
                .AddAspireFreeSql<IMainDatabase>(DataType.Sqlite, "Data Source = App_Data/main.db")
                .AddAspireSerilog(configuration => { })
                .AddAspireRequestLog(x => logHeaderKeys.Contains(x.Key))
                .AddAspireResponseLog()
                .AddResponseFormat()
                .AddAspireAuth<CustomAccount, CustomAccountManage>(provider =>
                    provider.GetRequiredService<CustomAccountManage>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAspireSwagger(typeof(Startup).Namespace);
            }

            app.UseFriendlyException();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

    public class CustomAccount : IAccount
    {
        public string CustomA { get; set; }
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string[] Roles { get; set; }
    }

    public class CustomAccountManage : IAccountManage<IAccount>
    {
        public IAccount GetAccountByIdAndPassword(string accountId, string password)
        {
            return new CustomAccount();
        }

        public string GetTokenByAccount(IAccount account)
        {
            return "1234";
        }

        public IAccount GetAccountByToken(string token)
        {
            return new CustomAccount();
        }
    }
}