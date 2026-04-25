using System.ComponentModel;
using System.Windows.Controls;
using Windows.Media.Control;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class MediaWidget : UpdateableWidget, INotifyPropertyChanged
{
    private MediaWidgetControl? _control;

    private GlobalSystemMediaTransportControlsSessionManager? _manager;
    private GlobalSystemMediaTransportControlsSession? _session;

    private string _title = "No media";
    private string _artist = "";
    private bool _isPlaying;

    public string Title
    {
        get => _title;
        set
        {
            if (_title == value)
            {
                return;
            }

            _title = value;
            PropertyChanged?.Invoke(this, new(nameof(Title)));
        }
    }

    public string Artist
    {
        get => _artist;
        set
        {
            if (_artist == value)
            {
                return;
            }

            _artist = value;
            PropertyChanged?.Invoke(this, new(nameof(Artist)));
        }
    }

    public bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            if (_isPlaying == value)
            {
                return;
            }

            _isPlaying = value;
            PropertyChanged?.Invoke(this, new(nameof(IsPlaying)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MediaWidget()
    {
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

        var mediaProps = await _session.TryGetMediaPropertiesAsync();
        var playback = _session.GetPlaybackInfo();

        Title = mediaProps.Title ?? "Unknown";
        Artist = mediaProps.Artist ?? "Unknown";
        IsPlaying = playback.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;

        // Todo get cover image

        _control?.Update(Title, Artist, IsPlaying);
    }

    public override UserControl GetControl()
    {
        _control = new MediaWidgetControl(this);
        return _control;
    }
}