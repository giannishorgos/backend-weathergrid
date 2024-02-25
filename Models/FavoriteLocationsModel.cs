using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models
{
    public class FavoriteLocationsModel
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
