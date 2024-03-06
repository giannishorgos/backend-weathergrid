using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherForecastAPI.Models;
using WeatherForecastAPI.Services;

namespace WeatherForecastAPI.Controllers
{
    /// <summary>
    /// Controller for retrieving weather data.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherDataService _weatherService;

        /// <summary>
        /// Creates a new instance, injecting <see cref="WeatherDataService"/>.
        /// </summary>
        /// <param name="weatherService">The HTTP service instance used for making HTTP requests.</param>
        public WeatherForecastController(WeatherDataService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Serves weather data according to <see cref="QueryParameters"/>.
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
        /// </summary>
        /// <param name="queryParameters">Options that defines the response.</param>
        /// <returns>Returns an <see cref="IActionResult"/> representing the HTTP response.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] QueryParameters queryParameters)
        {
            WeatherData? weatherData = await _weatherService.getWeatherData(queryParameters);
            if (weatherData != null)
            {
                return Ok(weatherData);
            }
            else
            {
                return BadRequest("Failed to retrieve weather data.");
            }
        }
    }
}
