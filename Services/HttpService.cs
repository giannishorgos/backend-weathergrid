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
            try 
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("I mean in service is the problem");
                throw;
            }
        }
    }
}
