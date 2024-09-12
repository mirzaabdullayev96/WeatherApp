namespace WeatherApp.Models.WeatherBit
{
    public class WeatherbitData
    {
        public DateTime Datetime { get; set; }
        public double Temp { get; set; }
        public WeatherbitWeather Weather { get; set; }
    }
}
