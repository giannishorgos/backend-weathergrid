namespace WeatherForecastAPI.Models
{
    /// <summary>
    /// Model for Weather API response.
    /// </summary>
    public class WeatherData {
        public Location? Location { get; set; }
        public CurrentWeather? Current { get; set; }
        public Forecast? Forecast { get; set; }
    }

    /// <summary>
    /// Model for location information contained in Weather API response.
    /// </summary>
    public class Location
    {
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string? Tz_id { get; set; }
        public int Localtime_epoch { get; set; }
        public string? Localtime { get; set; }
    }

    /// <summary>
    /// Model for current weather information contained in Weather API response.
    /// </summary>
    public class CurrentWeather
    {
        public int Last_updated_epoch { get; set; }
        public string? Last_updated { get; set; }
        public double Temp_c { get; set; }
        public double Temp_f { get; set; }
        public bool Is_day { get; set; }
        public WeatherCondition? Condition { get; set; }
        public AirQuality? Air_quality { get; set; }
    }

    /// <summary>
    /// Model for weather condition contained in Weather API response.
    /// </summary>
    public class WeatherCondition
    {
        public string? Text { get; set; }
        public string? Icon { get; set; }
        public int Code { get; set; }
    }

    /// <summary>
    /// Model for air quality contained in Weather API response.
    /// </summary>
    public class AirQuality
    {
        public double Co { get; set; }
        public double No2 { get; set; }
        public double O3 { get; set; }
        public double So3 { get; set; }
        public double PM2_5 { get; set; }
        public double PM10 { get; set; }
    }

    /// <summary>
    /// Model for forecast contained in Weather API response.
    /// </summary>
    public class Forecast
    {
        public List<ForecastDay>? Forecastday { get; set; }
    }

    /// <summary>
    /// Model for forecast day contained in Weather API response.
    /// </summary>
    public class ForecastDay
    {
        public DateOnly Date { get; set; }
        public string? Date_epoch { get; set; }
        public DayTemperatures? Day { get; set; }
        public List<HourlyTemperature>? Hour { get; set; }
    }

    /// <summary>
    /// Model for date contained in Weather API response.
    /// </summary>
    public class DayTemperatures 
    {
        public double Maxtemp_c { get; set; }
        public double Mintemp_c { get; set; }
    }

    /// <summary>
    /// Model for hourly temperature contained in Weather API response.
    /// </summary>
    public class HourlyTemperature
    {
        public string? Time { get; set; }
        public double Temp_c { get; set; }
    }
}
