using System.Text.Json.Serialization;

namespace ScreenStats.App.Weather.Models;

public class WeatherCurrentUnits
{
    [JsonPropertyName("temperature_2m")]
    public string? Temperature { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public string? WindSpeed { get; set; }
}
