using ScreenStats.App.Info.Models;
using ScreenStats.App.Info.Providers;

namespace ScreenStats.App.Info;

public static class SystemInfo
{
    public static RamInfoData GetRam()
    {
        return RamInfoProvider.Get();
    }

    public static DriveInfoData GetDrive(string drive)
    {
        return DriveInfoProvider.Get(drive);
    }

    public static MediaInfoData GetMedia()
    {
        return MediaInfoProvider.Get();
    }
}