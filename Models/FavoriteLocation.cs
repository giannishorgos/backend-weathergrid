using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models
{
    /// <summary>
    /// Represents a favorite location.
    /// </summary>
    public class FavoriteLocation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
