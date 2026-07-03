using System.Windows;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;

namespace ScreenStats.App.Widgets;

public static class WidgetRenderer
{
    public static void Render(StackPanel panel, LayoutConfig layout, List<Widget> widgets)
    {
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
