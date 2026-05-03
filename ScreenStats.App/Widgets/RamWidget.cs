using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets;

public class RamWidget(RamWidgetConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public RamWidgetConfig Config { get; } = config;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string UsageText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(UsageText)));
        }
    } = "";

    public double Usage
    {
        get;
        set
        {
            field = value;
            PropertyChanged?.Invoke(this, new(nameof(Usage)));
        }
    }

    public override void Update()
    {
        var ram = SystemInfo.GetRam();

        UsageText = $"{ram.UsedGb:0.0} GB / {ram.TotalGb:0.0} GB";
        Usage = (double)ram.UsedBytes / ram.TotalBytes * 100;
    }

    public override UserControl GetControl()
    {
        return new RamWidgetControl(this);
    }
}