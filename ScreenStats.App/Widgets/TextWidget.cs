using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.Widgets;

public class TextWidget(string content, string fontFamily, double fontSize) : UpdateableWidget, INotifyPropertyChanged
{
    private string _content = content;

    public string Content
    {
        get => _content;
        set
        {
            if (_content == value)
            {
                return;
            }

            _content = value;
            PropertyChanged?.Invoke(this, new(nameof(Content)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public string FontFamily { get; set; } = fontFamily;
    public double FontSize { get; set; } = fontSize;

    public override void Update()
    {
        Content += "1";
    }

    public override UserControl GetControl()
    {
        return new TextWidgetControl(this);
    }
}