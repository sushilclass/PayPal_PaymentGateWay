using GoodbyeFields_GAMC_DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_PaymentAPI.Helpers;
using Microsoft.Extensions.Logging;
using System.IO;
using Serilog;

namespace GoodbyeFields_GAMC_PaymentAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runt11111111111ime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors();
            services.AddControllers();
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<LogManagers.LogManagers>();
            // configure DI for application services
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<PayPalService>();
            services.Configure<AppSettings>(Configuration.GetSection("PayPalSettings"));

            var connection = this.Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<GoodbyeFieldsGAMCDBContext>(options => options.UseSqlServer(connection));
            services.AddTransient<GoodbyeFieldsGAMCDBContext>();

            //services.AddCors(options =>
            //{
            //    //options.AddPolicy("Policy1",
            //    //    builder =>
            //    //    {
            //    //        builder.WithOrigins("http://paypal.com",
            //    //                            "http://www.contoso.com");
            //    //    });
            //    options.AddPolicy("Policy1",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://paypal.com");
            //        });

            //    //options.AddPolicy("AnotherPolicy",
            //    //    builder =>
            //    //    {
            //    //        builder.WithOrigins("http://www.contoso.com")
            //    //                            .AllowAnyHeader()
            //    //                            .AllowAnyMethod();
            //    //    });
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
         
            app.UseRouting();
            app.UseAuthorization();

            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                            .Assembly.GetExecutingAssembly().Location) + "/Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                      .Assembly.GetExecutingAssembly().Location) + "/Logs/Error-{Date}.txt", LogLevel.Error);

            loggerFactory.AddSerilog();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Player}/{action=PaymentWithPaypal}/{id?}");
            });

            //app.UseCors();

        }
    }
}
