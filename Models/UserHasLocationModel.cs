namespace WeatherForecastAPI.Models
{
    public class UserHasLocation
    {
        public int UserId { get; set; }
        public int FavoriteLocationId { get; set; }

        public User User { get; set; } = null!;
        public FavoriteLocation FavoriteLocation { get; set; } = null!;
    }
}
