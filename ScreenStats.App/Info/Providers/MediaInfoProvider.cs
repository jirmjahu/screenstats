using Windows.Media.Control;
using ScreenStats.App.Helpers;
using ScreenStats.App.Info.Models;

namespace ScreenStats.App.Info.Providers;

public static class MediaInfoProvider
{
    private static GlobalSystemMediaTransportControlsSessionManager? _manager;

    public static async void Init()
    {
        _manager = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
    }

    public static MediaInfoData Get()
    {
        if (_manager == null)
        {
            return new MediaInfoData();
        }

        var session = _manager.GetCurrentSession();

        if (session == null)
        {
            return new MediaInfoData();
        }

        var mediaTask = session.TryGetMediaPropertiesAsync();
        var media = mediaTask.GetAwaiter().GetResult();
        var playback = session.GetPlaybackInfo();
        var timeline = session.GetTimelineProperties();

        return new MediaInfoData
        {
            Title = media.Title,
            Artist = media.Artist,
            IsPlaying = playback.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing,
            Position = timeline.Position,
            Duration = timeline.EndTime,
            App = session.SourceAppUserModelId,
            Thumbnail = ThumbnailHelper.GetThumbnail(media.Thumbnail)
        };
    }
}