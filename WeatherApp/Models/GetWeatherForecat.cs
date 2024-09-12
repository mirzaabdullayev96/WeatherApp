using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Domain
{
    public class GetWeatherForecast()
    {
        [Required]
        [FromRoute]
        public DateTime Date { get; set; }

        [Required]
        [FromQuery]
        public string City { get; set; }

        [Required]
        [FromQuery]
        public string Country { get; set; }
    }
}
