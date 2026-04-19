namespace ScreenStats.App.Config.Models;

public class CpuWidgetConfig : WidgetConfig
{
    public string Label { get; set; }
    public int UpdateInterval { get; set; } = 1000;
}