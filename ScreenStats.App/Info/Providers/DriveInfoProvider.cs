using System.IO;
using ScreenStats.App.Helpers;
using ScreenStats.App.Info.Models;

namespace ScreenStats.App.Info.Providers;

public static class DriveInfoProvider
{
    public static DriveInfoData Get(string drive)
    {
        var info = new DriveInfo(drive);

        var total = info.TotalSize;
        var free = info.AvailableFreeSpace;
        var used = total - free;

        return new DriveInfoData
        {
            Label = string.IsNullOrWhiteSpace(info.VolumeLabel) ? "Local Disk" : info.VolumeLabel,
            UsedPercentage = Math.Round(total == 0 ? 0 : (double)used / total * 100, 1),
            UsedGb = MemoryConverter.ToGb(used),
            FreeGb = MemoryConverter.ToGb(free),
            TotalGb = MemoryConverter.ToGb(total)
        };
    }
}