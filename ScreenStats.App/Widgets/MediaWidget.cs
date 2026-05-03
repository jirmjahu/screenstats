using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Media.Control;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class MediaWidget : UpdateableWidget, INotifyPropertyChanged
{
    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;

    public MediaWidgetConfig Config { get; }

    public MediaWidget(MediaWidgetConfig config)
    {
        Config = config;
        Init();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string DisplayText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(DisplayText)));
        }
    }

    public string Artist
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(Artist)));
        }
    }

    public string StatusText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(StatusText)));
        }
    }

    public BitmapImage? Thumbnail
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(Thumbnail)));
        }
    }

    private async void Init()
    {
        _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
    }

    public override async void Update()
    {
        if (_manager == null)
        {
            return;
        }

        _session = _manager.GetCurrentSession();
        if (_session == null)
        {
            return;
        }

        var media = await _session.TryGetMediaPropertiesAsync();
        var playback = _session.GetPlaybackInfo();
        var timeline = _session.GetTimelineProperties();

        var title = media.Title ?? "Unknown Title";
        var artist = media.Artist ?? "Unknown Artist";

        var isPlaying = playback.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
        var status = isPlaying ? "▶︎ Playing" : "⏸ Paused";

        Artist = artist;
        StatusText = status;
        Thumbnail = ThumbnailHelper.GetThumbnail(media.Thumbnail);

        var position = $"{timeline.Position:mm\\:ss}";
        var duration = $"{timeline.EndTime:mm\\:ss}";

        var app = _session.SourceAppUserModelId ?? "Unknown App";

        if (Config.Content == null)
        {
            return;
        }
        
        DisplayText = Config.Content
            .Replace("{title}", title)
            .Replace("{artist}", artist)
            .Replace("{status}", status)
            .Replace("{app}", app)
            .Replace("{position}", position)
            .Replace("{duration}", duration);
    }

    public override UserControl GetControl()
    {
        return new MediaWidgetControl(this);
    }
}