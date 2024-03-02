using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<FavoriteLocation> FavoriteLocation { get; set; }
        public DbSet<UserHasLocation> UserHasLocations { get; set; }

        public ApplicationDBContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserHasLocation>()
                .HasKey(entry => new { entry.UserId, entry.FavoriteLocationId });
        }
    }
}
