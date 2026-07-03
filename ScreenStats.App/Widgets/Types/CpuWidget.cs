using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Errors;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets.Types;

public class CpuWidget(CpuWidgetConfig config) : UpdateableWidget
{
    // moving this to the SystemInfo Class made the whole thing break, so I am leaving it here.
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

    public override List<Error> Validate()
    {
        var errors = new List<Error>();

        if (Config.Color != null && !ColorHelper.IsValidColor(Config.Color))
        {
            errors.Add(new Error("Invalid color in CPU widget"));
        }

        if (Config.ValueSize <= 0)
        {
            errors.Add(new Error("Invalid Font size in CPU widget (Font size has to be greater than 0)"));
        }

        return errors;
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