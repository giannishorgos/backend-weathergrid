using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public ICollection<UserHasLocation>? UserHasLocations { get; set; }
    }
}
