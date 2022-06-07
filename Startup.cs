using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KnowledgeControl.Services;
using KnowledgeControl.Entities;
using KnowledgeControl.Interfaces;
using System.Security.Claims;

namespace KnowledgeControl
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IAppSettings appSettings = new AppSettings(_configuration);

            if (_env.IsDevelopment())
            {
                services.AddCors();
            }

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<KCDbContext>(
                contextOptions =>
                    contextOptions
                        .UseSqlServer(
                        appSettings.KcConnectionString)
            );
            services.AddScoped<KCDbContext>();

            services.AddIdentity<User, IdentityRole<int>>(
                        options =>
                        {
                            options.Password.RequireDigit = false;
                            options.Password.RequireLowercase = false;
                            options.Password.RequireUppercase = false;
                            options.Password.RequireNonAlphanumeric = false;
                        })
                    .AddEntityFrameworkStores<KCDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(
                options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(
                    JwtBearerDefaults.AuthenticationScheme, options =>
                    {
                        options.SaveToken = true;

                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = false,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = false,

                            // будет ли валидироваться время существования
                            ValidateLifetime = false,

                            // установка ключа безопасности
                            IssuerSigningKey = Options.SecurityKey,

                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true
                        };
                    }
                );
            
            services.AddScoped<IHttpUserService, HttpUserService>();
            services.AddScoped<IAuthService, AuthService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                x => x.WithOrigins(new string[]{"http://localhost:5000","http://localhost:5001"}) // путь к нашему SPA клиенту
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        
            app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
