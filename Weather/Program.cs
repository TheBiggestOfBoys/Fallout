using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
	internal class Program
	{
		static readonly IConfigurationRoot config = new ConfigurationBuilder()
			.AddUserSecrets<Program>()
			.Build();

		static async Task Main()
		{

			if (WeatherService.IsInternetAvailable().Result)
			{
				Console.WriteLine("Internet connection available.");

				Console.OutputEncoding = Encoding.UTF8;

				string apiKey = config["WeatherApiKey"];

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
