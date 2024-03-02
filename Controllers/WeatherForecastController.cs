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
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpService _httpService;

        private const string _URL =
            "http://api.weatherapi.com/v1/forecast.json?key=5f7701a58bf44f1a8d9195220240401&alerts=nobbj&";

        /// <summary>
        /// Creates a new instance, injecting <see cref="ILogger"/> and <see cref="HttpService"/>.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="httpService">The HTTP service instance used for making HTTP requests.</param>
        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            HttpService httpService
        )
        {
            _logger = logger;
            _httpService = httpService;
        }

        /// <summary>
        /// Serves weather data according to <see cref="QueryParametersModel"/>.
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
        /// </summary>
        /// <param name="queryParameters">Options that defines the response.</param>
        /// <returns>Returns an <see cref="IActionResult"/> representing the HTTP response.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromQuery] QueryParametersModel queryParameters)
        {
            string uri =
                $"{_URL}q={queryParameters.City}&days={queryParameters.Days}&aqi={queryParameters.Aqi}";
            try
            {
                string response = await _httpService.getResponseString(uri);
                WeatherData? weatherData = JsonConvert.DeserializeObject<WeatherData>(response);
                if (weatherData != null)
                {
                    return Ok(weatherData);
                }
                else
                {
                    return BadRequest("Failed to deserialize Json");
                }
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Http Error: {e}");
                return BadRequest("No data for these parameters");
            }
        }
    }
}
