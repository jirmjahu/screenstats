using System.Windows.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class CpuWidgetControl : UserControl
{
    public CpuWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}