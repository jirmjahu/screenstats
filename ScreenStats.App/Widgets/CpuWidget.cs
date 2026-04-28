using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class CpuWidget(
    string label,
    string fontFamily,
    double size,
    double valueSize,
    string color,
    bool showBar)
    : UpdateableWidget, INotifyPropertyChanged
{
    private readonly PerformanceCounter _cpuCounter = new("Processor", "% Processor Time", "_Total");

    private float _usage;

    public string Label { get; set; } = label;
    public string FontFamily { get; set; } = fontFamily;
    public double Size { get; set; } = size;
    public double ValueSize { get; set; } = valueSize;
    public string Color { get; set; } = color;
    public bool ShowBar { get; set; } = showBar;

    public event PropertyChangedEventHandler? PropertyChanged;

    public float Usage
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
        Usage = (float)Math.Round(_cpuCounter.NextValue(), 1);
    }

    public override UserControl GetControl()
    {
        return new CpuWidgetControl(this);
    }
}