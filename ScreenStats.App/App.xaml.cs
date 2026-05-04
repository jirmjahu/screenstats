using System.Windows;
using System.Windows.Threading;
using ScreenStats.App.Config;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Info.Providers;
using ScreenStats.App.Widgets;

namespace ScreenStats.App;

public partial class App : Application
{
    private AppConfig? _config;
    private ConfigWatcher? _configWatcher;
    private readonly WidgetManager _widgetManager = new();
    private DispatcherTimer? _updateTimer;

    private MainWindow? _mainWindow;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        MediaInfoProvider.Init();

        _config = ConfigLoader.Load(AppPaths.ConfigFile);
        _configWatcher = new ConfigWatcher(AppPaths.ConfigFile, Reload);
        _widgetManager.Load(_config);

        _mainWindow = new MainWindow(_config, _widgetManager);
        _mainWindow.Show();
        
        _updateTimer = CreateUpdateTimer();
        _updateTimer.Start();
    }

    private void Reload()
    {
        Dispatcher.Invoke(() =>
        {
            _updateTimer?.Stop();
 
            _config = ConfigLoader.Load(AppPaths.ConfigFile);
            _widgetManager.Load(_config);
            _mainWindow?.Reload(_config);
 
            _updateTimer?.Start();
        });
    }

    private DispatcherTimer CreateUpdateTimer()
    {
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        timer.Tick += (_, _) => _widgetManager.UpdateWidgets();
        return timer;
    }
}