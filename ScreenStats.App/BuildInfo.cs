namespace ScreenStats.App;

public static class BuildInfo
{
    public enum AppMode
    {
        Portable,
        Installer,
        Unknown
    }

    public static AppMode GetMode()
    {
#if PORTABLE
        return AppMode.Portable;
#elif INSTALLER
        return AppMode.Installer;
#else
        return AppMode.Unknown;
#endif
    }
}