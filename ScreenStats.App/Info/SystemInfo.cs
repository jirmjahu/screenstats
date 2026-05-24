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

    public static async Task<WeatherInfoData?> GetWeather(string city, string country, string temperatureUnit, string windSpeedUnit)
    {
        return await WeatherInfoProvider.GetAsync(
            city,
            country,
            temperatureUnit,
            windSpeedUnit
        );
    }
}