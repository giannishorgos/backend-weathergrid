using System.Net;

namespace WeatherForecastAPI.Services
{
    /// <summary>
    /// Service for making HTTP requests.
    /// </summary>
    public class HttpService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Creates a new instance, setting up <see cref="HttpClient"/>.
        /// </summary>
        public HttpService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _client = new HttpClient();
        }


        /// <summary>
        /// Makes a GET request to the given URL and returns the response as a string.
        /// <exception cref="HttpRequestException">Thrown when the HTTP request fails.</exception>
        /// </summary>
        /// <param name="url">The URL to make the request to.</param>
        /// <returns>Returns the response as a string.</returns>
        public async Task<string> getResponseString(string url)
        {
            try 
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException e)
            {
                throw;
            }
        }
    }
}
