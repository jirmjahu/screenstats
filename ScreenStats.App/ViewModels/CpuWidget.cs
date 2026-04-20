using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.ViewModels;

public class CpuWidget : UpdateableWidget, INotifyPropertyChanged
{
    private readonly PerformanceCounter _cpuCounter;
    private float _usage;

    public string Label { get; set; }
    public double FontSize { get; set; }
    public double ValueFontSize { get; set; }

    public string Color { get; set; }
    public bool ShowProgress { get; set; }

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

    public CpuWidget(string label, double fontSize, double valueFontSize, string color, bool showProgress)
    {
        Label = label;
        FontSize = fontSize;
        ValueFontSize = valueFontSize;
        Color = color;
        ShowProgress = showProgress;

        _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        _cpuCounter.NextValue();
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