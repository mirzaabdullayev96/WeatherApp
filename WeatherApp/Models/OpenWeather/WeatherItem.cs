using Newtonsoft.Json;

namespace WeatherApp.Models.OpenWeather
{
    public class WeatherItem
    {
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        [JsonProperty("dt_txt")]
        public DateTime DtTxt { get; set; }
    }
}
