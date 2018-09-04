using ProductCatalog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProductDAL;

namespace ProductCatalogTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<ProductDBContext>(options =>
                options.UseInMemoryDatabase("integrationtestdb"));
        }

        public override void ConfigureSPA(IApplicationBuilder app, IHostingEnvironment env)
        {
            //not needed
        }
    }
}
