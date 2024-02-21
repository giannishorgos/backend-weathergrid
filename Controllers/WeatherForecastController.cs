using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models;
using WeatherForecastAPI.Services;
using Newtonsoft.Json;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController: ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly HttpService _httpService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) 
        {
            _logger = logger;
            _httpService = new HttpService();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get([FromQuery] string city) 
        {
            string url = $"http://api.weatherapi.com/v1/forecast.json?key=5f7701a58bf44f1a8d9195220240401&alerts=nobbj&q={city}";
            Task<string> response = _httpService.getResponseString(url);

            WeatherForecastModel model = JsonConvert.DeserializeObject<WeatherForecastModel>(response.Result);

            return Ok(model);
        }
    }
}
