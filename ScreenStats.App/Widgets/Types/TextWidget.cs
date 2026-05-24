using System.ComponentModel;
using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Text;

namespace ScreenStats.App.Widgets.Types;

public class TextWidget(TextWidgetConfig config) : UpdateableWidget, INotifyPropertyChanged
{
    public TextWidgetConfig Config { get; } = config;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string? DisplayText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            PropertyChanged?.Invoke(this, new(nameof(DisplayText)));
        }
    } = "";

    protected override Task Update()
    {
        DisplayText = PlaceholderReplacer.Replace(Config.Content);
        return Task.CompletedTask;
    }
    
    public override UserControl GetControl()
    {
        return new TextWidgetControl(this);
    }
    
    
    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromSeconds(1);
    }
    
}