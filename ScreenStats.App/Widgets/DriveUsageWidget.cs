using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class DriveUsageWidget(
    string drive,
    string content,
    string fontFamily,
    double fontSize,
    string color,
    bool showBar)
    : UpdateableWidget, INotifyPropertyChanged
{
    private float _usage;
    private string _displayText;

    public string FontFamily { get; set; } = fontFamily;
    public double FontSize { get; set; } = fontSize;
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

    public string DisplayText
    {
        get => _displayText;
        set
        {
            _displayText = value;
            PropertyChanged?.Invoke(this, new(nameof(DisplayText)));
        }
    }

    public override void Update()
    {
        var info = new DriveInfo(drive);

        double total = info.TotalSize;
        double free = info.AvailableFreeSpace;
        var used = total - free;
        var usedPercentage = (used / total) * 100;

        Usage = (float)Math.Round(usedPercentage, 1);

        var usedGb = MemoryConverter.ToGbString(used);
        var totalGb = MemoryConverter.ToGbString(total);
        var freeGb = MemoryConverter.ToGbString(free);
        
        var label = info.VolumeLabel;
        if (string.IsNullOrWhiteSpace(label))
        {
            label = "Local Disk";
        }

        DisplayText = content.Replace("\\n", Environment.NewLine)
            .Replace("{letter}", drive)
            .Replace("{label}", label)
            .Replace("{percent}", usedPercentage.ToString("0"))
            .Replace("{used}", usedGb)
            .Replace("{free}", freeGb)
            .Replace("{total}", totalGb);
    }

    public override UserControl GetControl()
    {
        return new DriveUsageControl(this);
    }
}