using Newtonsoft.Json;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using WeatherApp.Models.WeatherApi;
using WeatherApp.Utilities;

namespace WeatherApp.Services.Implementations
{
    public class WeatherApiService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public WeatherApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiKey = _configuration.GetSection("WeatherApiKeys:WeatherApi").Value;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync(DateTime? date, string city, string country)
        {

            var uriBuilder = new UriBuilder("http://api.weatherapi.com/v1/forecast.json")
            {
                Query = $"key={_apiKey}&q={city},{country}&days=4"
            };

            var weatherData = (await _httpClient.GetResponseAsync<WeatherApiResponse>(uriBuilder.Uri))?.Forecast?.Forecastday?.Where(x => x.Date <= date);
            var forecasts = new List<WeatherForecast>();

            if (weatherData == null) return forecasts;

            foreach (var item in weatherData)
            {
                foreach (var hour in item.Hour)
                {
                    // We could use AutoMapper here, but we have so little data that we did not implement it
                    forecasts.Add(new WeatherForecast
                    {
                        Date = hour.Time,
                        City = city,
                        Country = country,
                        TemperatureC = hour.TempC,
                        Summary = hour.Condition.Text,
                        Source = "WeatherAPI"
                    });
                }
            }

            return forecasts;
        }
    }
}
