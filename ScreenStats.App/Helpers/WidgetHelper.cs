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
                widgets.Add(new TextWidget(textConfig.Content, textConfig.FontFamily, textConfig.FontSize));
            }

            if (config is CpuWidgetConfig cpuConfig)
            {
                widgets.Add(new CpuWidget(cpuConfig.Label, cpuConfig.FontFamily, cpuConfig.FontSize,
                    cpuConfig.ValueFontSize, cpuConfig.Color, cpuConfig.ShowBar));
            }

            if (config is RamWidgetConfig ramConfig)
            {
                widgets.Add(new RamWidget(ramConfig.Label, ramConfig.FontFamily, ramConfig.FontSize,
                    ramConfig.ValueFontSize, ramConfig.Color, ramConfig.ShowBar));
            }

            if (config is MediaWidgetConfig mediaConfig)
            {
                widgets.Add(new MediaWidget(mediaConfig.ShowArtist,  mediaConfig.ShowStatus, mediaConfig.ShowThumbnail));
            }
        }

        return widgets;
    }
}