namespace ScreenStats.App.Config.Models;

public class RamWidgetConfig : WidgetConfig
{
    public string Label { get; set; }
    public int UpdateInterval { get; set; } = 1000;
}