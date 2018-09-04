using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ProductDAL;
using ProductServices.Services;
using AutoMapper;
using ProductServices.Mappers;
using System;
using System.IO;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using ProductServices.DTOs;

namespace ProductCatalog
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
            services.AddAutoMapper();
            services.AddOData();
            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureDatabase(services);

            services.AddTransient<IProductManagementService, ProductManagementService>();
            services.AddTransient<IProductValidationService, ProductValidationService>();
            services.AddTransient<IProductExportService, ProductExportService>();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ProductDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }*/

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                //ODataModelBuilder builder = new ODataConventionModelBuilder();
                //builder.EntitySet<ProductDTO>("Products");
                //routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
                routes.EnableDependencyInjection();
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            ConfigureSPA(app, env);
        }

        public virtual void ConfigureSPA(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";
                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
