using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Zoo_Management.Data;

namespace Zoo_Management
{
    public class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            CreateDbIfNotExists(host);
            
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ZooDbContext>();
            context.Database.EnsureCreated();

            if (!context.Species.Any())
            {
                var enclosures = SampleEnclosures.GetEnclosures().ToList();
                context.Enclosures.AddRange(enclosures);
                context.SaveChanges();
                
                var species = SampleSpecies.GetSpecies().ToList();
                context.Species.AddRange(species);
                context.SaveChanges();

                var animalsList = SampleAnimals.GetAnimals(species, enclosures).ToList();
                context.Animals.AddRange(animalsList);
                context.SaveChanges();

                var zooKeepers = SampleZooKeepers.GetZooKeepers(animalsList);
                context.ZooKeepers.AddRange(zooKeepers);
                context.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
