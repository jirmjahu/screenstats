using ScreenStats.App.Helpers;

namespace ScreenStats.App.Info.Models;

public class RamInfoData
{
    public long TotalBytes { get; init; }
    public long AvailableBytes { get; init; }
    
    public long UsedBytes => TotalBytes - AvailableBytes;

    public double TotalGb => MemoryConverter.ToGb(TotalBytes);
    public double AvailableGb => MemoryConverter.ToGb(AvailableBytes);
    public double UsedGb => MemoryConverter.ToGb(UsedBytes);
}