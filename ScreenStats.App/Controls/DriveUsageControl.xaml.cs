using System.Windows.Controls;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class DriveUsageControl : UserControl
{
    public DriveUsageControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}