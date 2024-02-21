using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController: ControllerBase
    {
        private static readonly string[] _summaries = new []
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) 
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastModel> Get() 
        {
            Console.WriteLine(Random.Shared.Next(4));
            return Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecastModel
                    {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = 25,
                     Summary = _summaries[Random.Shared.Next(_summaries.Length)]
                    })
                .ToArray();
        }
    }
}
