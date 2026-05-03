using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class RamWidget(RamWidgetConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    private readonly PerformanceCounter _ramCounter = new("Memory", "Available MBytes");

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
        // Source: https://stackoverflow.com/a/59073095
        var memoryInfo = GC.GetGCMemoryInfo();
        var totalBytes = memoryInfo.TotalAvailableMemoryBytes;

        var totalMb = MemoryConverter.ToMb(totalBytes);
        var availableMb = _ramCounter.NextValue();
        var usedMb = totalMb - availableMb;

        var usedGb = MemoryConverter.ToGbString(usedMb * 1024 * 1024);
        var totalGb = MemoryConverter.ToGbString(totalBytes);

        UsageText = $"{usedGb} / {totalGb}";
        Usage = (usedMb / totalMb) * 100;
    }

    public override UserControl GetControl()
    {
        return new RamWidgetControl(this);
    }
}