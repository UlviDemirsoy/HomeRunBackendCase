using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RatingService.Models;
using System;
using System.Linq;

namespace RatingService.Data
{
    public class PrepDb
    {

        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }

            if (!context.ServiceProviders.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.ServiceProviders.AddRange(
                    new Models.ServiceProvider() { Name = "Dot Net", Description = "Microsoft" },
                    new Models.ServiceProvider() { Name = "SQL Server Express", Description = "Microsoft" },
                    new Models.ServiceProvider() { Name = "Kubernetes", Description = "Cloud Native Computing Foundation" }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }


    }
}
