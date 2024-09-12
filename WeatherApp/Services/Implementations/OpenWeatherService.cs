using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;
using WeatherApp.Models.OpenWeather;
using WeatherApp.Utilities;

namespace WeatherApp.Services.Implementations
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public OpenWeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiKey = _configuration.GetSection("WeatherApiKeys:OpenWeather").Value;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync(DateTime? date, string city, string country)
        {

            var uriBuilder = new UriBuilder("https://api.openweathermap.org/data/2.5/forecast")
            {
                Query = $"q={city},{country}&appid={_apiKey}&units=metric"
            };

            var weatherData = (await _httpClient.GetResponseAsync<OpenWeatherResponse>(uriBuilder.Uri))?.List?.Where(data => data.DtTxt <= date);
            var forecasts = new List<WeatherForecast>();

            if (weatherData == null) return forecasts;

            foreach (var item in weatherData)
            {
                // We could use AutoMapper here, but we have so little data that we did not implement it
                forecasts.Add(new WeatherForecast
                {
                    Date = item.DtTxt,
                    City = city,
                    Country = country,
                    TemperatureC = item.Main.Temp,
                    Summary = item.Weather[0].Description,
                    Source = "OpenWeatherMap"
                });
            }
            return forecasts;
        }
    }
}
