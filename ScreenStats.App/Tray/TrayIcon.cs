using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ScreenStats.App.Tray;

public class TrayIcon : IDisposable
{
    private readonly NotifyIcon _notifyIcon;
    private readonly Action _onReload;
    private readonly Action _onExit;

    public TrayIcon(Action onReload, Action onExit)
    {
        _onReload = onReload;
        _onExit = onExit;

        _notifyIcon = new NotifyIcon
        {
            Icon = LoadIcon(),
            Text = "ScreenStats",
            Visible = true,
            ContextMenuStrip = BuildMenu()
        };
    }

    private ContextMenuStrip BuildMenu()
    {
        var menu = new ContextMenuStrip();

        menu.Items.Add("Open Config", null, (_, _) => OpenConfig());
        menu.Items.Add("Open Config Folder", null, (_, _) => OpenConfigFolder());
        menu.Items.Add("Reload Config", null, (_, _) => _onReload());
        menu.Items.Add(new ToolStripSeparator());
        menu.Items.Add("Exit", null, (_, _) => _onExit());

        return menu;
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

    private static Icon? LoadIcon()
    {
        var exePath = Environment.ProcessPath;

        return exePath == null ? SystemIcons.Application : Icon.ExtractAssociatedIcon(exePath);
    }

    public void Dispose()
    {
        _notifyIcon.Dispose();
    }
}