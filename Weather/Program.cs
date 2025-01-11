using System;
using System.Threading.Tasks;
using System.Text;

namespace Weather
{
    internal class Program
    {
        static async Task Main()
        {
            if (WeatherService.IsInternetAvailable().Result)
            {
                Console.WriteLine("Internet connection available.");

                Console.OutputEncoding = Encoding.UTF8;
                const string apiKey = "0852680cb401df381bedf177ee3ea4f9";

                Console.Write("Enter city name: ");
                string city = Console.ReadLine();

                WeatherService weatherService = new(apiKey);
                WeatherData? weatherData = await weatherService.GetWeatherDataAsync(city);

                if (weatherData != null)
                {
                    Console.WriteLine($"Weather in {city}:\n{weatherData.ToString(true)}");
                }
                else
                {
                    Console.WriteLine("Could not retrieve weather data.");
                }
            }
            else
            {
                Console.Error.WriteLine("No internet connection available.");
            }
        }
    }
}
