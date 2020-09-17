using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

namespace GoodbyeFields_GAMC
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
            services.AddCors();
            services.AddControllers();
            services.AddControllersWithViews();

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IPlayerWalletService, PlayerWalletService>();
            services.AddScoped<LogManagers.LogManagers>();
            services.AddScoped<PayPalService>();
            services.AddScoped<PlayerService>();

            var connection = this.Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<GoodbyeFieldsGAMCDBContext>(options => options.UseSqlServer(connection));
            services.AddTransient<GoodbyeFieldsGAMCDBContext>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1200);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
                
            services.AddRazorPages()
                .AddMvcOptions(options =>
                {
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "Please Enter the amount.");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            app.UseSession();
            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                            .Assembly.GetExecutingAssembly().Location) + "/Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                      .Assembly.GetExecutingAssembly().Location) + "/Logs/Error-{Date}.txt", LogLevel.Error);

            loggerFactory.AddSerilog();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
