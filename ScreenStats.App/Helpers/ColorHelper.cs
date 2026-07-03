using System.Windows.Media;

namespace ScreenStats.App.Helpers;

public static class ColorHelper
{
    public static bool IsValidColor(string? color)
    {
        if (string.IsNullOrWhiteSpace(color))
        {
            return false;
        }

        try
        {
            var converter = new BrushConverter();
            var result = converter.ConvertFromString(color);
            return result != null;
        }
        catch
        {
            return false;
        }
    }
}