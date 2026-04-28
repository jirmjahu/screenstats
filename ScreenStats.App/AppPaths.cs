using System.IO;

namespace ScreenStats.App;

public static class AppPaths
{
    private static readonly string BasePath = BuildInfo.GetMode() == BuildInfo.AppMode.Portable
        ? AppDomain.CurrentDomain.BaseDirectory
        : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ScreenStats");

    public static string ConfigDirectory => Path.Combine(BasePath, "config");
    public static string ConfigFile => Path.Combine(ConfigDirectory, "config.ini");
}