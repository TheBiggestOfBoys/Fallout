using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

public class WeatherData
{
	[JsonPropertyName("coord")]
	[JsonConverter(typeof(Vector2Converter))]
	public Vector2 Coordinates { get; set; } = new();

	public class Weather
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("main")]
		public string main { get; set; } = string.Empty;

		[JsonPropertyName("description")]
		public string Description { get; set; } = string.Empty;

		public string Icon => Id.ToString()[0] switch
		{
			'2' => "⛈️",
			'3' => "🌧️",
			'5' => "🌦️",
			'6' => "🌨️",
			'7' => "🌫️",
			_ => "☀️/🌕"
		};

		public override string ToString() => $"Weather: {main} ({Description}), Icon: {Icon}";
	}

	[JsonPropertyName("weather")]
	public Weather[] Weathers { get; set; } = [];

	[JsonPropertyName("base")]
	public string Base { get; set; } = string.Empty;

	public class Main
	{
		[JsonPropertyName("temp")]
		public float Temperature { get; set; }

		[JsonPropertyName("feels_like")]
		public float FeelsLike { get; set; }

		[JsonPropertyName("temp_min")]
		public float MinTemperature { get; set; }

		[JsonPropertyName("temp_max")]
		public float MaxTemperature { get; set; }

		[JsonPropertyName("pressure")]
		public int Pressure { get; set; }

		[JsonPropertyName("humidity")]
		public int Humidity { get; set; }

		[JsonPropertyName("sea_level")]
		public int SeaLevel { get; set; }

		[JsonPropertyName("grnd_level")]
		public int GroundLevel { get; set; }

		public override string ToString() =>
			$"Temperature: {Temperature}°F (Feels like: {FeelsLike}°F), Min: {MinTemperature}°F, Max: {MaxTemperature}°F" + Environment.NewLine +
			$"Pressure: {Pressure} hPa, Humidity: {Humidity}%" + Environment.NewLine +
			$"Sea Level: {SeaLevel} hPa, Ground Level: {GroundLevel} hPa";
	}

	[JsonPropertyName("main")]
	public Main MainData { get; set; } = new();

	[JsonPropertyName("visibility")]
	public int Visibility { get; set; }

	public class Wind
	{
		[JsonPropertyName("speed")]
		public float Speed { get; set; }

		[JsonPropertyName("deg")]
		public int Deg { get; set; }

		public override string ToString() =>
			$"Speed: {Speed} m/s, Direction: {Deg}°" + (Speed > 30 ? "💨" : string.Empty);
	}

	[JsonPropertyName("wind")]
	public Wind WindData { get; set; } = new();

	public class Clouds
	{
		[JsonPropertyName("all")]
		public int Cloudiness { get; set; }

		public string CloudIcon => Cloudiness switch
		{
			< 10 => "☀️",
			< 25 => "🌤️",
			< 50 => "⛅",
			_ => "☁️"
		};

		public override string ToString() => $"Cloudiness: {Cloudiness}%" + CloudIcon;
	}

	[JsonPropertyName("clouds")]
	public Clouds CloudData { get; set; } = new();

	[JsonPropertyName("dt")]
	[JsonConverter(typeof(UnixDateTimeConverter))]
	public DateTime DT { get; set; } = new();

	public class Sys
	{
		[JsonPropertyName("type")]
		public int Type { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; } = string.Empty;

		[JsonPropertyName("sunrise")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Sunrise { get; set; } = new();

		[JsonPropertyName("sunset")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Sunset { get; set; } = new();

		public override string ToString() =>
			$"Type: {Type}, ID: {Id}, Country: {Country}, Sunrise: {Sunrise}, Sunset: {Sunset}";
	}

	[JsonPropertyName("sys")]
	public Sys SysData { get; set; } = new();

	[JsonPropertyName("timezone")]
	public int Timezone { get; set; }

	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("cod")]
	public int Code { get; set; }

	public override string ToString() =>
		$"Location: {Name}, Coordinates: ({Coordinates.X}, {Coordinates.Y})" + Environment.NewLine +
		$"{string.Join(Environment.NewLine, (object[])Weathers)}" + Environment.NewLine +
		$"Base: {Base}" + Environment.NewLine +
		$"{MainData}" + Environment.NewLine +
		$"Visibility: {Visibility} meters" + Environment.NewLine +
		$"{WindData}" + Environment.NewLine +
		$"{CloudData}" + Environment.NewLine +
		$"Date/Time: {DT}" + Environment.NewLine +
		$"{SysData}" + Environment.NewLine +
		$"Timezone: {Timezone}, ID: {Id}, Code: {Code}";

	public string ToString(bool shorthand) =>
		MainData.Temperature + "°F, " + Environment.NewLine +
		WindData.Speed + "m/s" + Environment.NewLine +
		Weathers[0].Icon;

	public class UnixDateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).DateTime;
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(((DateTimeOffset)value).ToUnixTimeSeconds());
		}

	}

	public class Vector2Converter : JsonConverter<Vector2>
	{
		public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException();

			float lon = 0;
			float lat = 0;

			while (reader.Read())
			{
				if (reader.TokenType == JsonTokenType.EndObject)
					return new Vector2(lon, lat);

				if (reader.TokenType == JsonTokenType.PropertyName)
				{
					string propertyName = reader.GetString();
					reader.Read();

					switch (propertyName)
					{
						case "lon":
							lon = reader.GetSingle();
							break;
						case "lat":
							lat = reader.GetSingle();
							break;
					}
				}
			}

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
		{
			writer.WriteStartObject();
			writer.WriteNumber("lon", value.X);
			writer.WriteNumber("lat", value.Y);
			writer.WriteEndObject();
		}
	}
}
