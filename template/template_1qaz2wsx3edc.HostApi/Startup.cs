using System.Data;
using System.Reflection;
using AutoMapper;
using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddControllers();
            var appServiceAssembly = Assembly.Load($"{typeof(Startup).Namespace?.Replace("HostApi", "AppService")}");
            services.AddAspireDynamicWebApi(appServiceAssembly);
            services.AddAspireAutoMapper(appServiceAssembly);
            services.AddAspireFreeSql(DataType.Sqlite, "Data Source = App_Data/main.db");
            services.AddAspireSwagger(typeof(Startup).Namespace);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseAspireSwagger(typeof(Startup).Namespace);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}