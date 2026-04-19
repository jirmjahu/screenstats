using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Controls;

namespace ScreenStats.App.ViewModels;

public class TextWidget(string content, double fontSize) : UpdateableWidget, INotifyPropertyChanged
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