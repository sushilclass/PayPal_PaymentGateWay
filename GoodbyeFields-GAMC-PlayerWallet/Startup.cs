using GoodbyeFields_GAMC_BL;
using GoodbyeFields_GAMC_DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.IO;
using Serilog;

namespace GoodbyeFields_GAMC_PlayerWallet
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

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IPlayerWalletService, PlayerWalletService>();

            var connection = this.Configuration.GetConnectionString("DatabaseConnection");
            services.AddDbContext<GoodbyeFieldsGAMCDBContext>(options => options.UseSqlServer(connection));
            services.AddTransient<GoodbyeFieldsGAMCDBContext>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                            .Assembly.GetExecutingAssembly().Location) + "/Logs/Trace-{Date}.txt", LogLevel.Trace);
            loggerFactory.AddFile(Path.GetDirectoryName(System.Reflection
                      .Assembly.GetExecutingAssembly().Location) + "/Logs/Error-{Date}.txt", LogLevel.Error);

            loggerFactory.AddSerilog();

        }
    }
}
