using System.Windows.Controls;

namespace ScreenStats.App.Controls;

public partial class UsageBarControl : UserControl
{
    public UsageBarControl()
    {
        InitializeComponent();
    }

    public void SetValue(double percent)
    {
        percent = Math.Clamp(percent, 0, 100);

        var width = Grid.ActualWidth * (percent / 100);
        Fill.Width = width;
    }

    public void SetColor(string color)
    {
        Fill.Background = (System.Windows.Media.Brush)new System.Windows.Media.BrushConverter().ConvertFromString(color);
    }
}