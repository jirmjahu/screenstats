using System.Windows.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class MediaWidgetControl : UserControl
{
    public MediaWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}