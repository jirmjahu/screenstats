using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class RamWidget(
    string content,
    string fontFamily,
    double size,
    double valueSize,
    string color,
    bool showBar)
    : UpdateableWidget, INotifyPropertyChanged
{
    private readonly PerformanceCounter _ramCounter = new("Memory", "Available MBytes");

    private string _usageText = "";
    private double _usage;

    public string Content { get; set; } = content;
    public string FontFamily { get; set; } = fontFamily;
    public double Size { get; set; } = size;
    public double ValueSize { get; set; } = valueSize;
    public string Color { get; set; } = color;
    public bool ShowBar { get; set; } = showBar;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string UsageText
    {
        get => _usageText;
        set
        {
            if (_usageText == value) return;

            _usageText = value;
            PropertyChanged?.Invoke(this, new(nameof(UsageText)));
        }
    }

    public double Usage
    {
        get => _usage;
        set
        {
            _usage = value;
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