namespace WeatherForecastAPI.Interfaces
{
    public interface IHttp
    {
        Task<string> getResponseString(string url);
    }

}
