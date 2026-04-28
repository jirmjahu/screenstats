namespace ScreenStats.App.Helpers;

public static class MemoryConverter
{
    private const int Kb = 1024;
    private const int Mb = Kb * 1024;
    private const int Gb = Mb * 1024;

    public static double ToKb(double bytes) => bytes / Kb;
    public static double ToMb(double bytes) => bytes / Mb;
    public static double ToGb(double bytes) => bytes / Gb;

    public static string ToKbString(double bytes) => $"{ToKb(bytes):0.0} KB";
    public static string ToMbString(double bytes) => $"{ToMb(bytes):0.0} MB";
    public static string ToGbString(double bytes) => $"{ToGb(bytes):0.0} GB";
}