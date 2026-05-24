using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets.Types;

public class DriveUsageWidget(DriveUsageConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public DriveUsageConfig Config { get; } = config;

    public event PropertyChangedEventHandler? PropertyChanged;

    public double Usage
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

    protected override Task Update()
    {
        var drive = SystemInfo.GetDrive(Config.Drive!);

        Usage = drive.UsedPercentage;

        DisplayText = Config.Content!
            .Replace("\\n", Environment.NewLine)
            .Replace("{letter}", Config.Drive)
            .Replace("{label}", drive.Label)
            .Replace("{percent}", drive.UsedPercentage.ToString("0"))
            .Replace("{used}", $"{drive.UsedGb:0.0} GB")
            .Replace("{free}", $"{drive.FreeGb:0.0} GB")
            .Replace("{total}", $"{drive.TotalGb:0.0} GB");

        return Task.CompletedTask;
    }

    public override UserControl GetControl()
    {
        return new DriveUsageControl(this);
    }

    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromSeconds(30);
    }
}