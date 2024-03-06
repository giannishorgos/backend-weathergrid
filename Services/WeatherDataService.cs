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
        private readonly IConfiguration _configuration;
        private readonly string? _URL = string.Empty;

        /// <summary>
        /// Creates a new instance, setting up <see cref="HttpClient"/>.
        /// </summary>
        public WeatherDataService(IConfiguration configuration)
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _client = new HttpClient();

            _configuration = configuration;
            Console.WriteLine("Re bro kala den to pairnei?", _configuration.ToString());
            _URL = _configuration["WeatherAPIURL"];

        }

        /// <summary>
        /// Makes a GET request to the given URL and returns the response as a string.
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
        /// </summary>
        /// <param name="url">The URL to make the request to.</param>
        /// <returns>Returns the response as a string.</returns>
        public async Task<WeatherData?> getWeatherDataString(QueryParametersModel queryParameters)
        {
            string uri =
                $"{_URL}q={queryParameters.City}&days={queryParameters.Days}&aqi={queryParameters.Aqi}";

            Console.WriteLine("uri\n", uri);
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                response.EnsureSuccessStatusCode();

                string data =  await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherData>(data);
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
