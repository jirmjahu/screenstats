using System.Windows.Controls;
using ScreenStats.App.ViewModels;

namespace ScreenStats.App.Controls;

public partial class CpuWidgetControl : UserControl
{
    public CpuWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}