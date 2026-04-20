using System.Windows.Controls;

namespace ScreenStats.App.Widgets;

public abstract class Widget
{
    public abstract UserControl GetControl();
}