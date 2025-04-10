using System;
using System.Globalization;
using System.Net.Http;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml;

namespace Map
{
	internal class MapService(Vector2 location, float searchDistance)
	{
		Vector2 Location = location;
		float SearchDistance = searchDistance;

		private static readonly HttpClient client = new();
		private const string apiUrl = "https://overpass-api.de/api/interpreter";
		private const string lookupUrl = "https://www.openstreetmap.org/api/0.6/";
		public readonly bool Metric = new RegionInfo(CultureInfo.CurrentCulture.Name).IsMetric;

		private (Vector2, Vector2) GetBoundingBox(Vector2 location, float distance)
		{
			// Convert distance to kilometers if using imperial units
			float distanceInKm = Metric ? distance : ConvertMilesToKilometers(distance);

			float lat = location.X;
			float lon = location.Y;

			// Approximate values for degrees of latitude and longitude in kilometers
			const float kmPerDegreeLat = 111.0f;
			float kmPerDegreeLon = 111.0f * MathF.Cos(lat * MathF.PI / 180.0f);

			// Calculate latitude and longitude offsets
			float latOffset = distanceInKm / kmPerDegreeLat;
			float lonOffset = distanceInKm / kmPerDegreeLon;

			// Calculate min and max coordinates
			Vector2 minCoords = new(lat - latOffset, lon - lonOffset);
			Vector2 maxCoords = new(lat + latOffset, lon + lonOffset);

			return (minCoords, maxCoords);
		}

		private static float ConvertKilometersToMiles(float kilometers) => kilometers * 0.621371f;

		private static float ConvertMilesToKilometers(float miles) => miles / 0.621371f;

		public async Task<XmlDocument> GetMapDataAsync(Amenities amenity)
		{
			try
			{
				XmlDocument xmlDoc = new();
				(Vector2 minCoords, Vector2 maxCoords) = GetBoundingBox(Location, SearchDistance);
				string url = apiUrl + $"?data=[out:xml];(node[amenity={amenity}]({minCoords.X},{minCoords.Y},{maxCoords.X},{maxCoords.Y}););out body;";

				HttpResponseMessage response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();

				xmlDoc.LoadXml(await response.Content.ReadAsStringAsync());
				return xmlDoc;
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Error fetching map data: {ex.Message}");
				return new();
			}
		}

		public static async Task<XmlDocument> GetNodeDataAsync(int id)
		{
			try
			{
				XmlDocument xmlDoc = new();
				string url = lookupUrl + "node/" + id;

				HttpResponseMessage response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();

				xmlDoc.LoadXml(await response.Content.ReadAsStringAsync());
				return xmlDoc;
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"Error fetching node data: {ex.Message}");
				return new();
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
