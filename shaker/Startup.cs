using System;
using System.Collections.Generic;
using MessagePack;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shaker.Areas.Hubs;
using shaker.data.entity.Users;

namespace shaker
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.WithOrigins("http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials());
            });

            services.AddControllersWithViews();

            ConfigureServicesIoC(services);

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddSignalR(o => {
                    o.EnableDetailedErrors = true;
                })
                .AddMessagePackProtocol(options =>
                 {
                     options.FormatterResolvers = new List<IFormatterResolver>()
                    {
                        MessagePack.Resolvers.StandardResolver.Instance
                    };
                 });

            ConfigureAuth(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                ConfigureAuthApp(app, userManager);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "admin_areas",
                    areaName: "admin",
                    pattern: "admin/{controller}/{action}/{id?}",
                    defaults: new { controller = "AuthAdmin", action = "Login" });

                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Auth}/{action=Authenticate}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChannelHub>(ChannelHub.Path);
            });
        }
    }
}
