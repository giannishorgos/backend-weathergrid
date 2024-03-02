using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public ICollection<UserHasLocation>? UserHasLocations { get; set; }
    }
}
