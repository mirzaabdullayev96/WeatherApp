using Newtonsoft.Json;

namespace WeatherApp.Models.WeatherApi
{
    public class Hour
    {
        [JsonProperty("temp_c")]
        public double TempC { get; set; }
        public Condition Condition { get; set; }
        public DateTime Time { get; set; }
    }
}
