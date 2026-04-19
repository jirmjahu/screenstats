namespace ScreenStats.App.Config.Models;

public class AppConfig
{
    public BackgroundConfig Background { get; set; } = new();
    public Dictionary<string, WidgetConfig> Widgets { get; set; } = new();
}