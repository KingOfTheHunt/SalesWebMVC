using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Services;

namespace SalesWebMVC
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Faz com que os serviços injetados sejam acessados em todo o aplicativo.

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Injetando a dependência no aplicativo
            // Fazendo a conexão com o MySQL
            services.AddDbContext<SalesWebMVCContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("SalesWebMVCContext"), 
                    builder => builder.MigrationsAssembly("SalesWebMVC")));

            // Adicionando uma dependência.
            // Registra o SeedingService no sistema de injeção de dependência da aplicação.
            services.AddScoped<SeedingService>();

            // Adicionando a dependência do SellerService
            services.AddScoped<SellerService>();
            // Adicionando a dependência do DepartmentService
            services.AddScoped<DepartmentService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SeedingService seedingService)
        {
            // Verifica se o site está em desenvolvimento
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                seedingService.Seed();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    // Define qual página deve ser exibia ao acessar o site
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
