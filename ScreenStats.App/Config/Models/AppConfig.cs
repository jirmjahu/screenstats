namespace ScreenStats.App.Config.Models;

public class AppConfig
{
    public BackgroundConfig Background { get; } = new();
    public LayoutConfig Layout { get; } = new();
    public Dictionary<string, string> Defaults { get; set; } = new();
    public Dictionary<int, WidgetConfig> Widgets { get; } = new();
}