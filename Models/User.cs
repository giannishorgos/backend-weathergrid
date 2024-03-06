using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;

        public ICollection<UserHasLocation>? UserHasLocations { get; set; }
    }
}
