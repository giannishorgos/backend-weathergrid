namespace WeatherForecastAPI.Interfaces
{
    /// <summary>
    /// Interface for the HTTP client.
    /// </summary>
    public interface IHttp
    {
        Task<string> getResponseString(string url);
    }
}
