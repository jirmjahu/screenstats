using ScreenStats.App.Config.Models;
using ScreenStats.App.ViewModels;

namespace ScreenStats.App.Helpers;

public static class WidgetHelper
{
    
    public static List<Widget> GetWidgetsFromConfig(AppConfig appConfig)
    {
        var widgets = new List<Widget>();

        foreach (var config in appConfig.Widgets.Values)
        {
            if (config.Type == "text" && config is TextWidgetConfig textConfig)
            {
                widgets.Add(new TextWidget(textConfig.Content, textConfig.FontSize));
            }
            // TODO: Add other types
        }

        return widgets;
    }
}