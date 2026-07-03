using System.Windows.Controls;
using ScreenStats.App.Errors;

namespace ScreenStats.App.Widgets;

public abstract class Widget
{
    public abstract UserControl GetControl();

    public virtual List<Error> Validate()
    {
        return [];
    }
}