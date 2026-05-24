namespace ScreenStats.App.Config.Models;

public class WeatherWidgetConfig : WidgetConfig
{
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? TemperatureUnit { get; set; }
    public string? WindSpeedUnit { get; set; }
    public string? Content { get; set; }
    public string? Color { get; set; }
}
