
using AspNetCoreRateLimit;
using FluentValidation;
using FluentValidation.AspNetCore;
using StackExchange.Redis;
using WeatherApp.Services.Implementations;
using WeatherApp.Services.Interfaces;
using WeatherApp.Validators;

namespace WeatherApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient<IWeatherService, OpenWeatherService>();
            builder.Services.AddHttpClient<IWeatherService, WeatherApiService>();
            builder.Services.AddHttpClient<IWeatherService, WeatherbitService>();
            builder.Services.AddResponseCaching();

            builder.Services.AddMemoryCache();

            //limit requests services
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            //redis
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));
            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

            builder.Services.AddValidatorsFromAssemblyContaining<GetWeatherForecastValidator>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseIpRateLimiting();

            app.UseAuthorization();

            app.UseResponseCaching();


            app.MapControllers();

            app.Run();
        }
    }
}
