using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ScreenStats.App.Widgets;

namespace ScreenStats.App.Controls;

public partial class MediaWidgetControl : UserControl
{
    public MediaWidgetControl(Widget widget)
    {
        InitializeComponent();
        DataContext = widget;
    }

    public void Update(string title, string artist, bool isPlaying, BitmapImage? thumbnail)
    {
        TitleText.Text = title;
        ArtistText.Text = artist;
        StatusText.Text = isPlaying ? "▶︎ Playing" : "⏸ Paused";
        ThumbnailImage.Source = thumbnail;
    }
}