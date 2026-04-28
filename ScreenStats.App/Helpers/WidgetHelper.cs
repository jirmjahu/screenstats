using ScreenStats.App.Config.Models;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Helpers;

public static class WidgetHelper
{
    /// <summary>
    /// Creates widget objects from the app configuration
    /// </summary>
    /// <param name="appConfig">The configuration with widget settings</param>
    /// <returns>The list of created widgets</returns>
    public static List<Widget> CreateWidgetsFromConfig(AppConfig appConfig)
    {
        var widgets = new List<Widget>();

        foreach (var config in appConfig.Widgets.Values)
        {
            if (config is TextWidgetConfig textConfig)
            {
                widgets.Add(new TextWidget(textConfig.Content, textConfig.FontFamily, textConfig.Size));
            }

            if (config is CpuWidgetConfig cpuConfig)
            {
                widgets.Add(new CpuWidget(cpuConfig.Label, cpuConfig.FontFamily, cpuConfig.Size,
                    cpuConfig.ValueSize, cpuConfig.Color, cpuConfig.ShowBar));
            }

            if (config is RamWidgetConfig ramConfig)
            {
                widgets.Add(new RamWidget(ramConfig.Label, ramConfig.FontFamily, ramConfig.Size,
                    ramConfig.ValueSize, ramConfig.Color, ramConfig.ShowBar));
            }

            if (config is MediaWidgetConfig mediaConfig)
            {
                widgets.Add(new MediaWidget(mediaConfig.ShowArtist, mediaConfig.ShowStatus, mediaConfig.ShowThumbnail));
            }

            if (config is DriveUsageConfig driveUsageConfig)
            {
                widgets.Add(new DriveUsageWidget(driveUsageConfig.Drive, driveUsageConfig.Content,
                    driveUsageConfig.FontFamily, driveUsageConfig.Size,
                    driveUsageConfig.Color, driveUsageConfig.ShowBar));
            }
            
            
            
        }

        return widgets;
    }
}