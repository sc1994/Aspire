using System.Reflection;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Panda.DynamicWebApi;
using template_1qaz2wsx3edc.AppService.Accounts;
using template_1qaz2wsx3edc.Entity.MainDatabase;

#pragma warning disable 1591

namespace template_1qaz2wsx3edc.HostApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddRequestLog();

            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });
            
            var appServiceAssembly = Assembly.Load($"{typeof(Startup).Namespace?.Replace("HostApi", "AppService")}");
            services.AddAspireSwagger(typeof(Startup).Namespace);
            services.AddAspireAutoMapper(appServiceAssembly);
            services.AddAspireFreeSql<IMainDatabase>(DataType.Sqlite, "Data Source = App_Data/main.db");
            
            services.AddAspire(typeof(AccountAppService).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAspireSwagger(typeof(Startup).Namespace);
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}