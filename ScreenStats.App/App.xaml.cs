using System.Windows;
using ScreenStats.App.Config;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Info.Providers;
using ScreenStats.App.Tray;
using ScreenStats.App.Widgets;

namespace ScreenStats.App;

public partial class App : Application
{
    private AppConfig? _config;
    private ConfigWatcher? _configWatcher;
    private readonly WidgetManager _widgetManager = new();
    private TrayIcon? _trayIcon;

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

        _trayIcon = new TrayIcon(Reload, Shutdown);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _widgetManager.Stop();
        _trayIcon?.Dispose();

        base.OnExit(e);
    }

    private void Reload()
    {
        Dispatcher.Invoke(() =>
        {
            _config = ConfigLoader.Load(AppPaths.ConfigFile);
            _widgetManager.Load(_config);
            _mainWindow?.Reload(_config);
        });
    }
}
