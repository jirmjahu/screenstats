using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using H.NotifyIcon;

namespace ScreenStats.App.Tray;

public class TrayIcon : IDisposable
{
    private readonly TaskbarIcon _taskbarIcon;
    private readonly Action _onReload;
    private readonly Action _onExit;

    public TrayIcon(Action onReload, Action onExit)
    {
        _onReload = onReload;
        _onExit = onExit;

        _taskbarIcon = new TaskbarIcon
        {
            IconSource = new BitmapImage(new Uri("pack://application:,,,/Assets/icon.ico")),
            ToolTipText = "ScreenStats",
            ContextMenu = BuildMenu()
        };

        _taskbarIcon.ForceCreate();
    }

    private ContextMenu BuildMenu()
    {
        var menu = new ContextMenu();

        menu.Items.Add(CreateItem("Open Config", (_, _) => OpenConfig()));
        menu.Items.Add(CreateItem("Open Config Folder", (_, _) => OpenConfigFolder()));
        menu.Items.Add(CreateItem("Reload Config", (_, _) => _onReload()));
        menu.Items.Add(new Separator());
        menu.Items.Add(CreateItem("Exit", (_, _) => _onExit()));

        return menu;
    }

    private static MenuItem CreateItem(string header, RoutedEventHandler onClick)
    {
        var item = new MenuItem 
        {
            Header = header
        };
        item.Click += onClick;
        return item;
    }

    private static void OpenConfig()
    {
        var path = AppPaths.ConfigFile;

        if (!File.Exists(path))
        {
            return;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true
        });
    }

    private static void OpenConfigFolder()
    {
        var directory = AppPaths.ConfigDirectory;

        if (!Directory.Exists(directory))
        {
            return;
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = directory,
            UseShellExecute = true
        });
    }

    public void Dispose()
    {
        _taskbarIcon.Dispose();
    }
}
