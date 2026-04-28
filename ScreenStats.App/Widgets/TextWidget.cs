using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class TextWidget(string content, string fontFamily, double size) : Widget
{
    public string Content { get; set; } = content;
    public string FontFamily { get; set; } = fontFamily;
    public double Size { get; set; } = size;

    public override UserControl GetControl()
    {
        return new TextWidgetControl(this);
    }
}