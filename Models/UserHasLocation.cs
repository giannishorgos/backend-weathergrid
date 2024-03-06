namespace WeatherForecastAPI.Models
{
    /// <summary>
    /// Represents a user's favorite location.
    /// </summary>
    public class UserHasLocation
    {
        public string UserId { get; set; } = string.Empty;
        public int FavoriteLocationId { get; set; }

        public User User { get; set; } = null!;
        public FavoriteLocation FavoriteLocation { get; set; } = null!;
    }
}
