namespace ScreenStats.App.Config.Models;

public class RamWidgetConfig : WidgetConfig
{
    public string Label { get; set; }
    public double FontSize { get; set; }
    public double ValueFontSize { get; set; }
    public string Color { get; set; }
    public bool ShowBar { get; set; }
}