using Newtonsoft.Json;
using WeatherApp.Models;
using WeatherApp.Services.Interfaces;
using WeatherApp.Models.WeatherBit;
using WeatherApp.Models.WeatherApi;
using WeatherApp.Utilities;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace WeatherApp.Services.Implementations
{
    public class WeatherbitService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public WeatherbitService(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _apiKey = _configuration.GetSection("WeatherApiKeys:Weatherbit").Value;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecastAsync(DateTime? date, string city, string country)
        {
            var uriBuilder = new UriBuilder("https://api.weatherbit.io/v2.0/forecast/daily")
            {
                Query = $"city={city},{country}&key={_apiKey}"
            };

            var weatherData = (await _httpClient.GetResponseAsync<WeatherbitResponse>(uriBuilder.Uri))?.Data?.Where(data => data.Datetime <= date);
            var forecasts = new List<WeatherForecast>();

            if (weatherData == null) return forecasts;

            foreach (var item in weatherData)
            {
                // We could use AutoMapper here, but we have so little data that we did not implement it
                forecasts.Add(new WeatherForecast
                {
                    Date = item.Datetime,
                    City = city,
                    Country = country,
                    TemperatureC = item.Temp,
                    Summary = item.Weather.Description,
                    Source = "Weatherbit"
                });
            }

            return forecasts;
        }
    }
}
