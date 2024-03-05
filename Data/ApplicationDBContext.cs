using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Data
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<FavoriteLocation> FavoriteLocation { get; set; }
        public DbSet<UserHasLocation> UserHasLocations { get; set; }

        /// <summary>
        /// Creates a new instance, injecting <see cref="DbContextOptions"/>.
        /// </summary>
        public ApplicationDBContext(DbContextOptions options)
            : base(options) { }

        /// <summary>
        /// Configures the database context, creating a composite key for <see cref="UserHasLocation"/>.
        /// </summary>
        /// <param name="modelBuilder">The model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<UserHasLocation>()
                .HasKey(entry => new { entry.UserId, entry.FavoriteLocationId });
        }
    }
}
