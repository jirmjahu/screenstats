using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Helpers;
using ScreenStats.App.Widgets;

namespace ScreenStats.App;

public partial class MainWindow : Window
{
    private AppConfig _config;  
    private readonly WidgetManager _widgetManager;

    public MainWindow(AppConfig config, WidgetManager widgetManager)
    {
        InitializeComponent();
        _config = config;
        _widgetManager = widgetManager;

        ApplyConfig();
        RenderWidgets();
    }

    private void ApplyConfig()
    {
        var layout = _config.Layout;

        Width = layout.Width;
        Left = layout.Left;

        if (_config.Background.Enabled)
        {
            Background.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(_config.Background.Color);
            Background.Padding = new Thickness(_config.Background.Padding);
            Background.CornerRadius = new CornerRadius(_config.Background.CornerRadius);
        }
        else
        {
            Background.Background = Brushes.Transparent;
            Background.Padding = new Thickness(0);
            Background.CornerRadius = new CornerRadius(0);
        }

        Panel.Orientation = layout.Orientation?.ToLower() == "horizontal"
            ? Orientation.Horizontal
            : Orientation.Vertical;

        UpdateWindowPosition();
    }

    private void RenderWidgets()
    {
        Panel.Children.Clear();
        WidgetRenderer.Render(Panel, _config.Layout, _widgetManager.Widgets);
    }
    
    public void Reload(AppConfig config)
    {
        Dispatcher.Invoke(() =>
        {
            _config = config;
            ApplyConfig();
            RenderWidgets();
        });
    }
    
    private void UpdateWindowPosition()
    {
        var workArea = SystemParameters.WorkArea;

        Top = workArea.Bottom - _config.Layout.Bottom - ActualHeight;
        Left = _config.Layout.Left;

        if (Left < workArea.Left)
        {
            Left = workArea.Left;
        }

        if (Left + Width > workArea.Right)
        {
            Left = workArea.Right - Width;
        }

        if (Top < workArea.Top)
        {
            Top = workArea.Top;
        }

        if (Top + Height > workArea.Bottom)
        {
            Top = workArea.Bottom - Height;
        }
    }

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);
        WindowHelper.MakeDesktopWidget(this);
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateWindowPosition();
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        UpdateWindowPosition();
    }
}