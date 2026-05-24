using System.Text.Json.Serialization;

namespace ScreenStats.App.Weather.Models;

public class GeocodingResponse
{
    [JsonPropertyName("results")]
    public GeocodingResult[]? Results { get; set; }
}
