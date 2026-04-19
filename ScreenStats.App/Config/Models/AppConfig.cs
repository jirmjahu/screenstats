namespace ScreenStats.App.Config.Models;

public class AppConfig
{
    public BackgroundConfig Background { get; set; }
    public Dictionary<string, WidgetConfig> Widgets { get; set; }
}