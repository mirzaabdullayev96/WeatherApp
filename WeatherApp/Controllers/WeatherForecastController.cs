using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.ComponentModel.DataAnnotations;
using WeatherApp.Services.Implementations;
using WeatherApp.Services.Interfaces;
using WeatherApp.Models;
using WeatherApp.Domain;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WeatherForecastController : ControllerBase
    {
        private readonly IEnumerable<IWeatherService> _weatherServices;
        private readonly IDatabase _cache;


        public WeatherForecastController(IEnumerable<IWeatherService> weatherServices, IConnectionMultiplexer redis)
        {
            _weatherServices = weatherServices;
            _cache = redis.GetDatabase();
        }


        [HttpGet("{Date}")]
        [ProducesResponseType<List<WeatherForecast>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = ["city", "country"], NoStore = false)]
        public async Task<IActionResult> GetWeatherForecast(GetWeatherForecast weatherForecast)
        {
            string cacheKey = $"weatherforecast:{weatherForecast.Date:yyyy-MM-dd}-{weatherForecast.City}-{weatherForecast.Country}".ToLower();

            var cachedForecast= await _cache.StringGetAsync(cacheKey);
            if (cachedForecast.HasValue) return Ok(JsonConvert.DeserializeObject<List<WeatherForecast>>(cachedForecast));

            var allForecasts = new List<WeatherForecast>();
            var tasks = _weatherServices.Select(async service =>
            {
                try
                {
                    var forecasts = await service.GetWeatherForecastAsync(weatherForecast.Date, weatherForecast.City, weatherForecast.Country);
                    return forecasts;
                }
                catch (Exception)
                {
                    return Enumerable.Empty<WeatherForecast>(); 
                }
            });

            var results = await Task.WhenAll(tasks);

            allForecasts.AddRange(results.SelectMany(forecasts => forecasts));

            if (allForecasts.Count == 0) return NotFound(new ErrorResponse("No weather forecast could be found for the specified city or country."));

            var cacheExpiration = TimeSpan.FromHours(1);
            await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(allForecasts), cacheExpiration);

            return Ok(allForecasts);
        }
    }
}
