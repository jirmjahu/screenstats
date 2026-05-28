using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets.Types;

public class CpuWidget(CpuWidgetConfig config) : UpdateableWidget
{
    // Moving it to the SystemInfo Class made the whole thing break, so I am leaving it here.
    private readonly PerformanceCounter _cpuCounter = new("Processor", "% Processor Time", "_Total");

    public CpuWidgetConfig Config { get; } = config;

    public float Usage
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
        Usage = (float)Math.Round(_cpuCounter.NextValue(), 1);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _cpuCounter.Dispose();
        base.Dispose();
    }

    public override UserControl GetControl()
    {
        return new CpuWidgetControl(this);
    }
    
    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromSeconds(1);
    }
}