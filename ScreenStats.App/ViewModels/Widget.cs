using System.Windows.Controls;

namespace ScreenStats.App.ViewModels;

public abstract class Widget
{
    public abstract UserControl GetControl();
}