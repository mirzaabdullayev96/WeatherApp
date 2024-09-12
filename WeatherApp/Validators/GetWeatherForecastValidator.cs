using FluentValidation;
using WeatherApp.Domain;
using WeatherApp.Utilities;

namespace WeatherApp.Validators
{
    public class GetWeatherForecastValidator : AbstractValidator<GetWeatherForecast>
    {
        public GetWeatherForecastValidator()
        {
            RuleFor(x => x.Country).Length(2).Matches("^[a-zA-Z]+$");
            RuleFor(x => x.City).NotEmpty().Matches("^[a-zA-Z]+$");
            RuleFor(x => x.Date).NotEmpty().Must(date => date.IsWithinRange(0, 3));
        }
    }
}
