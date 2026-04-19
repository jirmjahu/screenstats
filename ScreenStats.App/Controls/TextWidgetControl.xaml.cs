using System.Windows.Controls;
using ScreenStats.App.ViewModels;

namespace ScreenStats.App.Controls;

public partial class TextWidgetControl : UserControl
{
    public TextWidgetControl(TextWidget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }
}