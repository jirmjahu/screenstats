using System.Windows.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class WeatherWidgetControl : UserControl
{
    public WeatherWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}
