using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ScreenStats.App.Controls;

public partial class UsageBarControl : UserControl
{
    public UsageBarControl()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(
            nameof(Value),
            typeof(double),
            typeof(UsageBarControl),
            new PropertyMetadata(0.0, OnValueChanged));

    public static readonly DependencyProperty BarColorProperty =
        DependencyProperty.Register(
            nameof(BarColor),
            typeof(string),
            typeof(UsageBarControl),
            new PropertyMetadata("#32CD32", OnColorChanged));

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public string BarColor
    {
        get => (string)GetValue(BarColorProperty);
        set => SetValue(BarColorProperty, value);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (UsageBarControl)d;
        control.UpdateFill();
    }

    private void UpdateFill()
    {
        var percent = Math.Clamp(Value, 0, 100);
        Fill.Width = Grid.ActualWidth * (percent / 100);
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (UsageBarControl)d;
        control.UpdateColor();
    }

    private void UpdateColor()
    {
        Fill.Background = (Brush)new BrushConverter().ConvertFromString(BarColor);
    }
}