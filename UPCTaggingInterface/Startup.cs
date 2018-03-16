using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceConfiguration;
using Microsoft.EntityFrameworkCore;
using UPCTaggingInterface.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace UPCTaggingInterface
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
            var connectionString = Configuration[Common.CommonUtilities.Constants.PostgresqlConnStr];
            services.AddEntityFrameworkNpgsql().AddDbContext<Repository.UPCTaggingDBContext>(options => options.UseNpgsql(connectionString,b=>b.MigrationsAssembly("UPCTaggingInterface")));

            services.RegisterServices();

            services.AddCors();
            services.AddMvc();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.AccessDeniedPath = "";
                        options.LoginPath = "/api/login/authenticate-user";
                        options.Events.OnRedirectToLogin = (context) =>
                          {
                              context.Response.StatusCode = 401;
                              return Task.CompletedTask;
                          };

                    });
                    

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //    options.LoginPath = "/login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
            //    options.LogoutPath = "/logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
            //    options.AccessDeniedPath = ""; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
            //    options.SlidingExpiration = true;
            //});

            //services.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;

            //    // Lockout settings
            //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //    options.Lockout.MaxFailedAccessAttempts = 10;
            //    options.Lockout.AllowedForNewUsers = true;

            //    // User settings
            //    options.User.RequireUniqueEmail = true;
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            app.UseAuthentication();
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMvc();
           
            app.UseDefaultFiles();
            app.UseStaticFiles();

        }

    }
}
