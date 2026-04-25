using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Windows.Media.Control;
using ScreenStats.App.Controls;
using ScreenStats.App.Helpers;

namespace ScreenStats.App.Widgets;

public class MediaWidget : UpdateableWidget, INotifyPropertyChanged
{
    private MediaWidgetControl? _control;

    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;

    private string _title;
    private string _artist;
    private string _statusText;
    private BitmapImage? _thumbnail;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value) return;

            _title = value;
            PropertyChanged?.Invoke(this, new(nameof(Title)));
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

    public bool ShowArtist { get; }
    public bool ShowStatus { get; }
    public bool ShowThumbnail { get; }

    public MediaWidget(bool showArtist, bool showStatus, bool showThumbnail)
    {
        ShowArtist = showArtist;
        ShowStatus = showStatus;
        ShowThumbnail = showThumbnail;

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

        Title = media.Title ?? "Unknown";
        Artist = media.Artist ?? "Unknown";

        var isPlaying = playback.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
        StatusText = isPlaying ? "▶︎ Playing" : "⏸ Paused";

        Thumbnail = ThumbnailHelper.GetThumbnail(media.Thumbnail);
    }

    public override UserControl GetControl()
    {
        _control = new MediaWidgetControl(this);
        return _control;
    }
}