using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class CpuWidget(
    string label,
    string fontFamily,
    double fontSize,
    double valueFontSize,
    string color,
    bool showBar)
    : UpdateableWidget, INotifyPropertyChanged
{
    private CpuWidgetControl _control;

    private readonly PerformanceCounter _cpuCounter = new("Processor", "% Processor Time", "_Total");

    private float _usage;

    public string Label { get; set; } = label;
    public string FontFamily { get; set; } = fontFamily;
    public double FontSize { get; set; } = fontSize;
    public double ValueFontSize { get; set; } = valueFontSize;
    public string Color { get; set; } = color;
    public bool ShowBar { get; set; } = showBar;

    public event PropertyChangedEventHandler? PropertyChanged;

    public float Usage
    {
        get => _usage;
        set
        {
            if (_usage == value)
            {
                return;
            }

            _usage = value;
            PropertyChanged?.Invoke(this, new(nameof(Usage)));
        }
    }

    public override void Update()
    {
        Usage = (float)Math.Round(_cpuCounter.NextValue(), 1);
        _control.Update(Usage, Color);
    }

    public override UserControl GetControl()
    {
        _control = new CpuWidgetControl(this);
        return _control;
    }
}