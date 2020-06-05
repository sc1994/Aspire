
using Aspire.Identity;

using Demo.Application.Blogs;
using Demo.Database.MainDb;
using Demo.Database.UserIdentity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.Service
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
            var application = typeof(BolgAppService).Assembly;

            var database = typeof(MainDbContext).Assembly;
            services.AddAspireEfCore(database);

            services.AddAspireSwagger();
            services.AddAspireAutoMapper(application);
            services.AddAspireIdentity<UserIdentityDbContext>(Configuration, optionsAction =>
            {
                optionsAction.UseSqlite("Data Source = D:/SqliteDbs/aspire_user_identity_db.db");
            });

            services.AddAspireController(application);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAspireSwagger();

            app.UseAspireIdentity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
