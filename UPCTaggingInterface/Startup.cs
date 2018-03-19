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
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Http;
using Common.CommonEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Authentication;

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
            services.AddEntityFrameworkNpgsql().AddDbContext<Repository.UPCTaggingDBContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("UPCTaggingInterface")));

            services.RegisterServices();

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            services.AddCors();
            services.AddMvc();


            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(options =>
            //        {
            //            options.AccessDeniedPath = "";
            //            options.LoginPath = "/api/login/authenticate-user";
            //            options.Cookie.HttpOnly = false;
            //            options.Events.OnRedirectToLogin = (context) =>
            //              {
            //                  context.Response.StatusCode = 401;
            //                  return Task.CompletedTask;
            //              };
            //            options.Cookie.SecurePolicy = CookieSecurePolicy.None;
            //        });

            var settings = Configuration.GetSection("Authentication:Security");
            services.Configure<AuthSettings>(settings);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                          .AddJwtBearer(options =>
                          {
                              options.TokenValidationParameters = new TokenValidationParameters
                              {
                                  ValidateIssuer = true,
                                  ValidateAudience = true,
                                  ValidateLifetime = true,
                                  ValidateIssuerSigningKey = true,

                                  ValidIssuer = settings["ClientName"],
                                  ValidAudience = settings["ClientName"],
                                  IssuerSigningKey = TokenProvider.GenerateSecret(settings["SecretKey"])
                              };
                          });

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

            //var options = new RewriteOptions()
            //  .AddRedirectToHttps();

            //app.UseRewriter(options);

            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();

        }

    }
}
