using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;
using System;
using System.Linq;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
        }

        private static void SeedData(AppDbContext appDbContext, bool isProd)
        {
            if(isProd) {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    appDbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            Console.WriteLine("--> Seeding Data...");

            if (appDbContext is null) throw new ArgumentNullException(nameof(appDbContext));
            if (appDbContext.Platforms is null) throw new ArgumentNullException(nameof(appDbContext.Platforms));

            if (appDbContext.Platforms.Any())
            {
                Console.WriteLine("--> Data already seeded");
                return;
            }

            appDbContext.Platforms.AddRange(
                new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );

            appDbContext.SaveChanges();
            Console.WriteLine("--> Data seeded");
        }
    }
}
