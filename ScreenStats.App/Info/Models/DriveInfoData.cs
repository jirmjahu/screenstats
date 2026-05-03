namespace ScreenStats.App.Info.Models;

public class DriveInfoData
{
    public string? Label { get; init; }
    public double UsedPercentage  { get; init; }
    public double UsedGb { get; init; }
    public double FreeGb { get; init; }
    public double TotalGb { get; init; }
}