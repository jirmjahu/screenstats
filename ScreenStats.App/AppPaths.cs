using System.IO;

namespace ScreenStats.App;

public static class AppPaths
{
    private static readonly string BasePath = BuildInfo.GetMode() == BuildInfo.AppMode.Installer
        ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ScreenStats")
        : AppDomain.CurrentDomain.BaseDirectory;

    public static string ConfigDirectory => Path.Combine(BasePath, "config");
    public static string ConfigFile => Path.Combine(ConfigDirectory, "config.ini");
}