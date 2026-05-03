using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class CpuWidget(CpuWidgetConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public CpuWidgetConfig Config { get; } = config;
    
    private readonly PerformanceCounter _cpuCounter = new("Processor", "% Processor Time", "_Total");

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
        Usage = (float)Math.Round(_cpuCounter.NextValue(), 1);
    }

    public override UserControl GetControl()
    {
        return new CpuWidgetControl(this);
    }
}