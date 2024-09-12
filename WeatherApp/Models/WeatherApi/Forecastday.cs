namespace WeatherApp.Models.WeatherApi
{
    public class Forecastday
    {
        public DateTime Date { get; set; }
        public List<Hour> Hour { get; set; }
    }
}
