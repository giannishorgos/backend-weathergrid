using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models;
using WeatherForecastAPI.Services;
using Newtonsoft.Json;

namespace WeatherForecastAPI.Controllers
{
    /// <summary>
    /// Controller for retrieving weather data based on query parameters.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController: ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpService _httpService;

        private const string _url =  "http://api.weatherapi.com/v1/forecast.json?key=5f7701a58bf44f1a8d9195220240401&alerts=nobbj&";

        /// <summary>
        /// Creates a new instance, injecting <see cref="ILogger"/> and <see cref="HttpService"/>
        /// </summary>
        public WeatherForecastController(ILogger<WeatherForecastController> logger, HttpService httpService) 
        {
            _logger = logger;
            _httpService = httpService; 
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] QueryParametersModel queryParameters) 
        {
            string uri = $"{_url}q={queryParameters.City}&days={queryParameters.Days}&aqi={queryParameters.Aqi}";
            try
            {
                string response = await _httpService.getResponseString(uri);
                WeatherData? weatherData = JsonConvert.DeserializeObject<WeatherData>(response);
                if(weatherData != null)
                {
                    return Ok(weatherData);
                }
                else 
                {
                    return BadRequest("Failed to deserialize Json");
                }
            }
            catch(HttpRequestException e)
            {
                _logger.LogError($"Http Error: {e}");
                return BadRequest("No data for these parameters");
            }


        }
    }
}
