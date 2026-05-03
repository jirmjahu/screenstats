using System.Diagnostics;
using ScreenStats.App.Info.Models;

namespace ScreenStats.App.Info.Providers;

public static class CpuInfoProvider
{
    private static readonly PerformanceCounter CpuCounter = new("Processor", "% Processor Time", "_Total");

    public static CpuInfoData Get()
    {
        return new CpuInfoData
        {
            UsagePercentage = (float)Math.Round(CpuCounter.NextValue(), 1)
        };
    }
}