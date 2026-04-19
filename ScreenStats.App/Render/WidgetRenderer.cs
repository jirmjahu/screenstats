using System.Windows;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.ViewModels;

namespace ScreenStats.App.Render;

public static class WidgetRenderer
{
    public static void Render(Window window, Border background, StackPanel panel, AppConfig config, List<Widget> widgets)
    {
        var layout = config.Layout;

        for (var i = 0; i < widgets.Count; i++)
        {
            var widget = widgets[i];
            var control = widget.GetControl();

            var isLast = i == widgets.Count - 1;
            if (!isLast)
            {
                control.Margin = panel.Orientation == Orientation.Vertical
                    ? new Thickness(0, 0, 0, layout.Spacing)
                    : new Thickness(0, 0, layout.Spacing, 0);
            }

            panel.Children.Add(control);
        }
    }
}