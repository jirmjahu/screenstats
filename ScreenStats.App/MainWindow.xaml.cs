using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ScreenStats.App.Config;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Helpers;
using ScreenStats.App.Render;
using ScreenStats.App.Widgets;

namespace ScreenStats.App;

public partial class MainWindow : Window
{
    private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
    private const string ConfigPath = "config.ini";

    private AppConfig _config;
    private readonly ConfigWatcher _configWatcher;
    private readonly List<Widget> _widgets;
    private readonly DispatcherTimer _updateTimer;

    public MainWindow()
    {
        InitializeComponent();

        _config = ConfigLoader.Load(BasePath, ConfigPath);
        _configWatcher = new ConfigWatcher(Path.Combine(BasePath, ConfigPath), ReloadConfig);
        _widgets = WidgetHelper.CreateWidgetsFromConfig(_config);

        ApplyConfig();
        RenderWidgets();

        _updateTimer = CreateUpdateTimer();
        _updateTimer.Start();
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

        Panel.Orientation = layout.Orientation?.ToLower() == "horizontal"
            ? Orientation.Horizontal
            : Orientation.Vertical;

        UpdateWindowPosition();
    }

    private void ReloadConfig()
    {
        Dispatcher.Invoke(() =>
        {
            _config = ConfigLoader.Load(BasePath, ConfigPath);

            _widgets.Clear();
            _widgets.AddRange(WidgetHelper.CreateWidgetsFromConfig(_config));

            ApplyConfig();
            RenderWidgets();
        });
    }

    private void RenderWidgets()
    {
        Panel.Children.Clear();
        WidgetRenderer.Render(Panel, _config.Layout, _widgets);
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