using System.Windows.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class RamWidgetControl : UserControl
{
    public RamWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}