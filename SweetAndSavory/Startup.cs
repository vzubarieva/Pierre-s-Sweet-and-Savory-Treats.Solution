using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SweetAndSavory.Models;
using Microsoft.AspNetCore.Identity;

namespace SweetAndSavory
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services
                .AddEntityFrameworkMySql()
                .AddDbContext<SweetAndSavoryContext>(
                    options =>
                        options.UseMySql(
                            Configuration["ConnectionStrings:DefaultConnection"],
                            ServerVersion.AutoDetect(
                                Configuration["ConnectionStrings:DefaultConnection"]
                            )
                        )
                );
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SweetAndSavoryContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                routes =>
                {
                    routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );

            app.UseStaticFiles();
            app.Run(
                async (context) =>
                {
                    await context.Response.WriteAsync("Hello World!");
                }
            );
        }
    }
}
