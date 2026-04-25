using System.IO;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;

namespace ScreenStats.App.Helpers;

public static class ThumbnailHelper
{
    public static BitmapImage? GetThumbnail(IRandomAccessStreamReference? thumbnailStreamReference)
    {
        if (thumbnailStreamReference == null)
        {
            return null;
        }

        using var stream = thumbnailStreamReference
            .OpenReadAsync()
            .AsTask()
            .GetAwaiter()
            .GetResult();

        using var memoryStream = new MemoryStream();
        stream.AsStreamForRead().CopyTo(memoryStream);

        memoryStream.Position = 0;

        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.StreamSource = memoryStream;
        image.EndInit();
        image.Freeze();

        return image;
    }
}