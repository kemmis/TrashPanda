using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrashPanda.Service;
using TrashPanda.Core.Contracts;
using AutoMapper;
using TrashPanda.Data.SqlServer;
using EntityFramework.DbContextScope.Interfaces;
using EntityFramework.DbContextScope;
using Microsoft.EntityFrameworkCore;

namespace TrashPanda
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
            services.AddMvc();
            services.AddAutoMapper();

            #region EF / SqlServer

            services.AddDbContext<TrashPandaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            #region register dependencies

            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ITrashPandaDataProvider, SqlServerTrashPandaDataProvider>();
            services.AddTransient<IAmbientDbContextLocator, AmbientDbContextLocator>();
            services.AddTransient<IDbContextScopeFactory, DbContextScopeFactory>();
            services.AddTransient<ScopedDataProviderBaseDependencies>();
            services.AddTransient<IDbContextFactory, TrashPandaDbContextFactory>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ITrashPandaDataProvider trashPandaDataProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            trashPandaDataProvider.Init();
        }
    }
}
