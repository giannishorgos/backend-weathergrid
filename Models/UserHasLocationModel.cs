namespace WeatherForecastAPI.Models
{
    public class UserHasLocation
    {
        public string UserId { get; set; } = string.Empty;
        public int FavoriteLocationId { get; set; }

        public User User { get; set; } = null!;
        public FavoriteLocation FavoriteLocation { get; set; } = null!;
    }
}
