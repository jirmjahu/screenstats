using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class RamWidget(
    string label,
    string fontFamily,
    double fontSize,
    double valueFontSize,
    string color,
    bool showBar)
    : UpdateableWidget, INotifyPropertyChanged
{
    private RamWidgetControl _control;

    private readonly PerformanceCounter _ramCounter = new("Memory", "Available MBytes");

    private string _usageText = "";

    public string Label { get; set; } = label;
    public string FontFamily { get; set; } = fontFamily;
    public double FontSize { get; set; } = fontSize;
    public double ValueFontSize { get; set; } = valueFontSize;
    public string Color { get; set; } = color;
    public bool ShowBar { get; set; } = showBar;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string UsageText
    {
        get => _usageText;
        set
        {
            if (_usageText == value)
            {
                return;
            }

            _usageText = value;
            PropertyChanged?.Invoke(this, new(nameof(UsageText)));
        }
    }

    public override void Update()
    {
        // Source: https://stackoverflow.com/a/59073095
        var gcMemoryInfo = GC.GetGCMemoryInfo();
        var installedMemory = gcMemoryInfo.TotalAvailableMemoryBytes;
        var totalMb = installedMemory / 1048576.0;

        var availableMb = _ramCounter.NextValue();
        var usedMb = totalMb - availableMb;
        var usedGb = usedMb / 1024f;
        var totalGb = totalMb / 1024f;

        UsageText = $"{usedGb:0.0} / {totalGb:0.0} GB";

        var usedPercent = (usedMb / totalMb) * 100;
        _control.Update(usedPercent, Color);
    }

    public override UserControl GetControl()
    {
        _control = new RamWidgetControl(this);
        return _control;
    }
}