using System;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Weather
{
    public class WeatherService
    {
        private static readonly HttpClient client = new();
        private readonly string apiKey;
        private const string apiUrl = "http://api.openweathermap.org/data/2.5/weather";
        public readonly bool Metric;

        public WeatherService(string key)
        {
            apiKey = key;
            Metric = new RegionInfo(CultureInfo.CurrentCulture.Name).IsMetric;
        }

        public async Task<WeatherData?> GetWeatherDataAsync(string city)
        {
            try
            {
                string url = $"{apiUrl}?q={city}&appid={apiKey}&units={(Metric ? "metric" : "imperial")}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                WeatherData? weatherResponse = JsonSerializer.Deserialize<WeatherData>(responseBody);

                return weatherResponse;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }

        public static async Task<bool> IsInternetAvailable()
        {
            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync("https://www.google.com");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
