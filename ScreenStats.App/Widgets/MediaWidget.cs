using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Media.Control;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class MediaWidget : UpdateableWidget, INotifyPropertyChanged
{
    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;

    private string _displayText;
    private string _artist;
    private string _statusText;
    private BitmapImage? _thumbnail;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string DisplayText
    {
        get => _displayText;
        set
        {
            if (_displayText == value) return;

            _displayText = value;
            PropertyChanged?.Invoke(this, new(nameof(DisplayText)));
        }
    }

    public string Artist
    {
        get => _artist;
        set
        {
            if (_artist == value) return;

            _artist = value;
            PropertyChanged?.Invoke(this, new(nameof(Artist)));
        }
    }

    public string StatusText
    {
        get => _statusText;
        set
        {
            if (_statusText == value) return;

            _statusText = value;
            PropertyChanged?.Invoke(this, new(nameof(StatusText)));
        }
    }

    public BitmapImage? Thumbnail
    {
        get => _thumbnail;
        set
        {
            if (_thumbnail == value) return;

            _thumbnail = value;
            PropertyChanged?.Invoke(this, new(nameof(Thumbnail)));
        }
    }

    private string Content { get; }
    public string Color { get; }
    public string FontFamily { get; }
    public double Size { get; }
    public bool ShowArtist { get; }
    public bool ShowStatus { get; }
    public bool ShowThumbnail { get; }
    public double ThumbnailSize { get; }

    public MediaWidget(string content, string color, string fontFamily, double size, bool showArtist, bool showStatus,
        bool showThumbnail, double thumbnailSize)
    {
        Content = content;
        Color = color;
        FontFamily = fontFamily;
        Size = size;
        ShowArtist = showArtist;
        ShowStatus = showStatus;
        ShowThumbnail = showThumbnail;
        ThumbnailSize = thumbnailSize;

        Init();
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

        DisplayText = Content
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