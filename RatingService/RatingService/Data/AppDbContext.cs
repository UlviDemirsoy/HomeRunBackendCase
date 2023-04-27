using Microsoft.EntityFrameworkCore;
using RatingService.Models;

namespace RatingService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Rating> Ratings { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Creating One to Many Relationship
            modelBuilder
                .Entity<ServiceProvider>()
                .HasMany(s => s.Ratings)
                .WithOne(r => r.ServiceProvider)
                .HasForeignKey(s => s.ServiceProviderId);

            modelBuilder
                .Entity<Rating>()
                .HasOne(r => r.ServiceProvider)
                .WithMany(s => s.Ratings)
                .HasForeignKey(s => s.ServiceProviderId);

        }


    }
}
