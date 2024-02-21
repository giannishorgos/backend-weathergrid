using System.Net;

namespace WeatherForecastAPI.Services
{
    public class HttpService
    {
        private readonly HttpClient _client;

        public HttpService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            };

            _client = new HttpClient();
        }


        public async Task<string> getResponseString(string url)
        {
            HttpResponseMessage response = await _client.GetAsync(url);
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}
