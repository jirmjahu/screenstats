using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Errors;
using ScreenStats.App.Text;

namespace ScreenStats.App.Widgets.Types;

public class TextWidget(TextWidgetConfig config) : UpdateableWidget
{
    public TextWidgetConfig Config { get; } = config;

    public string? DisplayText
    {
        get;
        set
        {
            if (field == value) return;

            field = value;
            OnPropertyChanged();
        }
    } = "";

    public override List<Error> Validate()
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(Config.Content))
        {
            errors.Add(new Error("No content specified in Text widget"));
        }

        return errors;
    }

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