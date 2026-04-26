using System.IO;

namespace ScreenStats.App.Config;

public class ConfigWatcher
{
    private readonly FileSystemWatcher _watcher;

    public ConfigWatcher(string filePath, Action onChanged)
    {
        var directory = Path.GetDirectoryName(filePath);
        var fileName = Path.GetFileName(filePath);

        _watcher = new FileSystemWatcher(directory ?? string.Empty)
        {
            Filter = fileName,
            EnableRaisingEvents = true,
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName
        };

        _watcher.Changed += (_, _) => onChanged();
    }
}