using System.Net;
using WeatherForecastAPI.Interfaces;
using WeatherForecastAPI.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace WeatherForecastAPI.Services
{
    /// <summary>
    /// Service for making HTTP requests.
    /// </summary>
    public class WeatherDataService 
    {
        private readonly HttpClient _client;
        private string? _URL = string.Empty;
        private ILogger _logger;

        /// <summary>
        /// Creates a new instance, injecting <see cref="IConfiguration"/> and <see cref="ILogger"/>.
        /// </summary>
        /// <param name="configuration">The configuration instance used for retrieving configuration data.</param>
        /// <param name="logger">The logger instance used for logging.</param>
        public WeatherDataService(IConfiguration configuration, 
            ILogger<WeatherDataService> logger)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _client = new HttpClient();

            _logger = logger;
            _URL = configuration["WeatherAPIURL"];

        }

        /// <summary>
        /// Retrieves weather data according to <see cref="QueryParameters"/>.
        /// </summary>
        /// <param name="queryParameters">Options that defines the response.</param>
        /// <returns>Returns a <see cref="WeatherData"/> instance representing the weather data or null.</returns>
        public async Task<WeatherData?> getWeatherData(QueryParameters queryParameters)
        {
            if (_URL is null)
            {
                _URL = "http://api.weatherapi.com/v1/forecast.json?key=5f7701a58bf44f1a8d9195220240401&alerts=nobbj&";
            }
            string uri =
                $"{_URL}q={queryParameters.City}&days={queryParameters.Days}&aqi={queryParameters.Aqi}";

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                string data =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherData>(data);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError(e, "Failed to retrieve weather data.");
                return null;
            }
        }
    }
}
