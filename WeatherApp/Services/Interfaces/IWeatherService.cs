using WeatherApp.Models;

namespace WeatherApp.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync(DateTime? date, string city, string country);
    }
}
