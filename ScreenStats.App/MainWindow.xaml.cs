using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Errors;
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
        // these are the default values if the config has errors
        double width = 750;
        double left = 20;
        var color = "#90000000";
        double padding = 15;
        double cornerRadius = 10;
        var backgroundEnabled = true;
        var orientation = "vertical";

        // if the config is fine, try to apply it
        if (!ErrorManager.HasErrors())
        {
            var layout = _config.Layout;

            width = layout.Width;
            left = layout.Left;
            color = _config.Background.Color;
            padding = _config.Background.Padding;
            cornerRadius = _config.Background.CornerRadius;
            backgroundEnabled = _config.Background.Enabled;
            orientation = _config.Layout.Orientation;
        }

        Width = width;
        Left = left;

        if (backgroundEnabled)
        {
            Background.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(color);
            Background.Padding = new Thickness(padding);
            Background.CornerRadius = new CornerRadius(cornerRadius);
        }
        else
        {
            Background.Background = Brushes.Transparent;
            Background.Padding = new Thickness(0);
            Background.CornerRadius = new CornerRadius(0);
        }

        Panel.Orientation = orientation.ToLower() == "horizontal"
            ? Orientation.Horizontal
            : Orientation.Vertical;

        UpdateWindowPosition();
    }

    private void RenderWidgets()
    {
        Panel.Children.Clear();
        
        if (ErrorManager.HasErrors())
        {
            foreach (var error in ErrorManager.Errors)
            {
                Panel.Children.Add(new ErrorControl(error.Message));
            }
            return;
        }
        
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