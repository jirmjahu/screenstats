using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets.Types;

public class RamWidget(RamWidgetConfig config) : UpdateableWidget
{
    public RamWidgetConfig Config { get; } = config;

    public string UsageText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    } = "";

    public double Usage
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged();
        }
    }

    protected override Task Update()
    {
        var ram = SystemInfo.GetRam();

        UsageText = $"{ram.UsedGb:0.0} GB / {ram.TotalGb:0.0} GB";
        Usage = (double)ram.UsedBytes / ram.TotalBytes * 100;
        return Task.CompletedTask;
    }

    public override UserControl GetControl()
    {
        return new RamWidgetControl(this);
    }
    
    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromSeconds(1);
    }
    
}