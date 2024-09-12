using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Models.OpenWeather;

namespace WeatherApp.Utilities
{
    public static class HttpClientExtension
    {
        public static async Task<T?> GetResponseAsync<T>(this HttpClient httpClient, Uri uri)
        {
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
