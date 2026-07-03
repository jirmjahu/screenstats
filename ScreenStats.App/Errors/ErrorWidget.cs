using System.Windows.Controls;
using ScreenStats.App.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Errors;

public class ErrorWidget(string message) : Widget
{
    public override UserControl GetControl()
    {
        return new ErrorControl(message);
    }
}