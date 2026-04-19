namespace ScreenStats.App.Config.Models;

public class AppConfig
{
    public BackgroundConfig Background { get; } = new();
    public LayoutConfig Layout { get; } = new();
    public Dictionary<string, WidgetConfig> Widgets { get; } = new();
}