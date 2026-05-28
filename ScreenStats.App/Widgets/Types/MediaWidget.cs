using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets.Types;

public class MediaWidget(MediaWidgetConfig config) : UpdateableWidget
{
    public MediaWidgetConfig Config { get; } = config;

    public string? DisplayText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    }

    public string? Artist
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    }

    public string? StatusText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    }

    public BitmapImage? Thumbnail
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    }

    protected override Task Update()
    {
        var media = SystemInfo.GetMedia();

        var title = media.Title ?? "Unknown Title";
        var artist = media.Artist ?? "Unknown Artist";
        var status = media.IsPlaying ? "▶︎ Playing" : "⏸ Paused";

        Artist = artist;
        StatusText = status;
        Thumbnail = media.Thumbnail;

        var position = $"{media.Position:mm\\:ss}";
        var duration = $"{media.Duration:mm\\:ss}";

        var app = media.App ?? "Unknown App";

        if (Config.Content == null)
        {
            return Task.CompletedTask;
        }

        DisplayText = Config.Content
            .Replace("{title}", title)
            .Replace("{artist}", artist)
            .Replace("{status}", status)
            .Replace("{app}", app)
            .Replace("{position}", position)
            .Replace("{duration}", duration);

        return Task.CompletedTask;
    }

    public override UserControl GetControl()
    {
        return new MediaWidgetControl(this);
    }
    
    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromSeconds(1);
    }
    
}