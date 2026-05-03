using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class TextWidget(TextWidgetConfig config) : Widget
{
    public TextWidgetConfig Config { get; } = config;

    public override UserControl GetControl()
    {
        return new TextWidgetControl(this);
    }
}