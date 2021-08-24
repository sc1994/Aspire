using System.Reflection;
using Aspire;
using Aspire.Domain.Account;
using FreeSql;
using Serilog;

using template_1qaz2wsx3edc.Domain.Accounts;
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
            services.AddScoped<AccountManage>();

            var appServiceAssembly = Assembly.Load($"{typeof(Startup).Namespace?.Replace("HostApi", "AppService")}");
            var appDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            services
                .AddAspire(appServiceAssembly)
                .AddAspireSwagger(typeof(Startup).Namespace)
                .AddAspireAutoMapper(appServiceAssembly)
                .AddAspireFreeSql<IMainDatabase>(DataType.Sqlite,
                    $"Data Source = {Path.Combine(appDataPath, "main.db")}")
                .AddAspireSerilog(configuration =>
                {
                    configuration.MinimumLevel.Information();

#if DEBUG
                    configuration.WriteTo.Console();
                    configuration.WriteTo.Elasticsearch(
                        "http://10.88.4.43:9200/",
                        "aspire-{0:yyyy.MM}", 
                        inlineFields: true);
#endif
                    configuration.WriteTo.File(Path.Combine(appDataPath, "logs", ".log"),
                        rollingInterval: RollingInterval.Day);
                })
                .AddAspireRequestLog(x => logHeaderKeys.Contains(x.Key))
                .AddAspireResponseLog()
                .AddResponseFormat()
                .AddAspireAuth<Domain.Accounts.Account, AccountManage>(provider =>
                    provider.GetRequiredService<AccountManage>());
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
}