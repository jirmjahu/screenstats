namespace ScreenStats.App.Config.Models;

public class MediaWidgetConfig : WidgetConfig
{
    public string? Content { get; set; }
    public string? Color { get; set; }
    public bool? ShowArtist { get; set; }
    public bool? ShowStatus { get; set; }
    public bool? ShowThumbnail { get; set; }
    public double? ThumbnailSize { get; set; }
    
}
