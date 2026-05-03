using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class DriveUsageWidget(DriveUsageConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public DriveUsageConfig Config { get; } = config;
    
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

    public string? DisplayText
    {
        get;
        set
        {
            field = value;
            PropertyChanged?.Invoke(this, new(nameof(DisplayText)));
        }
    }

    public override void Update()
    {
        var info = new DriveInfo(Config.Drive!);

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

        DisplayText = Config.Content!.Replace("\\n", Environment.NewLine)
            .Replace("{letter}", Config.Drive)
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