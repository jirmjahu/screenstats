using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets;

public class CpuWidget(CpuWidgetConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public CpuWidgetConfig Config { get; } = config;

    public event PropertyChangedEventHandler? PropertyChanged;

    public float Usage
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
        Usage = SystemInfo.GetCpu().UsagePercentage;
    }

    public override UserControl GetControl()
    {
        return new CpuWidgetControl(this);
    }
}