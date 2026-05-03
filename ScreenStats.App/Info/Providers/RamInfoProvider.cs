using System.Diagnostics;
using ScreenStats.App.Info.Models;

namespace ScreenStats.App.Info.Providers;

public static class RamInfoProvider
{
    private static readonly PerformanceCounter RamCounter = new("Memory", "Available MBytes");

    public static RamInfoData Get()
    {
        var memoryInfo = GC.GetGCMemoryInfo(); // https://stackoverflow.com/a/59073095

        return new RamInfoData
        {
            TotalBytes = memoryInfo.TotalAvailableMemoryBytes,
            AvailableBytes = (long)RamCounter.NextValue() * 1024 * 1024
        };
    }
}