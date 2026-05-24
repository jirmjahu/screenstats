using ScreenStats.App.Weather;

namespace ScreenStats.App.Info.Models;

public class WeatherInfoData
{
    public double Temperature { get; init; }
    public double ApparentTemperature { get; init; }
    public double Humidity { get; init; }
    public double WindSpeed { get; init; }
    public int WeatherCode { get; init; }
    public bool IsDay { get; init; }
    public string? TemperatureUnit { get; init; }
    public string? WindSpeedUnit { get; init; }
    public string? LocationName { get; set; }

    public string Description => WeatherCodes.Description(WeatherCode);
    public string Icon => WeatherCodes.Icon(WeatherCode, IsDay);
}