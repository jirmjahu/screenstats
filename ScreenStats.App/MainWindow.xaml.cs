using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ScreenStats.App.Config;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Helpers;
using ScreenStats.App.Render;
using ScreenStats.App.ViewModels;

namespace ScreenStats.App;

public partial class MainWindow : Window
{
    private readonly AppConfig _config;
    private readonly List<Widget> _widgets;
    private readonly DispatcherTimer _updateTimer;

    public MainWindow()
    {
        InitializeComponent();

        _config = ConfigLoader.Load("/", "config.ini");
        _widgets = WidgetHelper.CreateWidgetsFromConfig(_config);
        
        var layout = _config.Layout;
        
        Width = layout.Width;
        Left = layout.Left;
        
        if (_config.Background.Enabled)
        {
            Background.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(_config.Background.Color);
        }

        Panel.Orientation = layout.Orientation?.ToLower() == "horizontal"
            ? Orientation.Horizontal
            : Orientation.Vertical;

        Panel.Children.Clear();
        
        WidgetRenderer.Render(Panel, _config.Layout, _widgets);

        _updateTimer = CreateUpdateTimer();
        _updateTimer.Start();
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

    private DispatcherTimer CreateUpdateTimer()
    {
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        timer.Tick += (_, _) => UpdateWidgets();
        return timer;
    }
    
    private void UpdateWidgets()
    {
        foreach (var widget in _widgets)
        {
            if (widget is UpdateableWidget updateable)
            {
                updateable.Update();
            }
        }
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
}