using System.Windows.Media.Imaging;

namespace ScreenStats.App.Info.Models;

public class MediaInfoData
{
    public string? Title { get; init; }
    public string? Artist { get; init; }
    public bool IsPlaying { get; init; }
    public TimeSpan Position { get; init; }
    public TimeSpan Duration { get; init; }
    public string? App { get; init; }
    public BitmapImage? Thumbnail { get; init; }
}