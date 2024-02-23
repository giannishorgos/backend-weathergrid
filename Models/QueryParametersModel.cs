using System.ComponentModel.DataAnnotations;

namespace WeatherForecastAPI.Models 
{
    /// <summary>
    /// Model for query parameters.
    /// </summary>
    public class QueryParametersModel 
    {
        private string _aqi = "no";

        [Required]
        public string City { get; set; } = string.Empty;
        public int Days { get; set; } = 1;
        public string Aqi 
        {
            get 
            {
                return _aqi;
            }
            set
            {
                value = value.ToLower();
                if(value == "yes" || value == "no")
                {
                    _aqi = value;
                }
            }
        }
    }
}
