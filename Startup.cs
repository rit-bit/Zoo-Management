using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog;
using Zoo_Management.Repositories;

namespace Zoo_Management
{
    public class Startup
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string CorsPolicyName = "_zooCorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ZooDbContext>(options =>
            {
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                options.UseSqlite("Data Source=zoo.db");
            });

            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services
                .AddControllers()
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services
                .AddTransient<IAnimalsRepo, AnimalsRepo>()
                .AddTransient<ISpeciesRepo, SpeciesRepo>()
                .AddTransient<IZooKeepersRepo, ZooKeepersRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Zoo_Management", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoo_Management v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(CorsPolicyName);

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}