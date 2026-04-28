namespace ScreenStats.App.Config.Models;

public class CpuWidgetConfig : WidgetConfig
{
    public string Label { get; set; }
    public double ValueSize { get; set; }
    public string Color { get; set; }
    public bool ShowBar { get; set; }
}