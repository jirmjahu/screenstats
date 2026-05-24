using System.Text.Json.Serialization;

namespace ScreenStats.App.Weather.Models;

public class WeatherResponse
{
    [JsonPropertyName("current")]
    public WeatherCurrent? Current { get; set; }

    [JsonPropertyName("current_units")]
    public WeatherCurrentUnits? CurrentUnits { get; set; }
}
