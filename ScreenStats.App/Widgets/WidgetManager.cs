using ScreenStats.App.Config.Models;
using ScreenStats.App.Errors;
using ScreenStats.App.Widgets.Types;

namespace ScreenStats.App.Widgets;

public class WidgetManager
{
    public List<Widget> Widgets { get; } = [];

    public void Load(AppConfig config)
    {
        Stop();
        Widgets.Clear();
        Widgets.AddRange(CreateWidgetsFromConfig(config));

        foreach (var updateable in Widgets.OfType<UpdateableWidget>())
        {
            updateable.Start();
        }
    }

    public void Stop()
    {
        foreach (var updateable in Widgets.OfType<UpdateableWidget>())
        {
            updateable.Dispose();
        }
    }

    private List<Widget> CreateWidgetsFromConfig(AppConfig appConfig)
    {
        var widgets = new List<Widget>();

        foreach (var config in appConfig.Widgets.Values)
        {
            ApplyDefaults(config, appConfig.Defaults);

            switch (config)
            {
                case TextWidgetConfig textConfig:
                    widgets.Add(new TextWidget(textConfig));
                    break;
                case CpuWidgetConfig cpuConfig:
                    widgets.Add(new CpuWidget(cpuConfig));
                    break;
                case RamWidgetConfig ramConfig:
                    widgets.Add(new RamWidget(ramConfig));
                    break;
                case MediaWidgetConfig mediaConfig:
                    widgets.Add(new MediaWidget(mediaConfig));
                    break;
                case DriveUsageConfig driveUsageConfig:
                    widgets.Add(new DriveUsageWidget(driveUsageConfig));
                    break;
                case WeatherWidgetConfig weatherConfig:
                    widgets.Add(new WeatherWidget(weatherConfig));
                    break;
                default:
                    ErrorManager.Add($"Unknown widget type: {config.GetType()}");
                    break;
            }
        }
        
        // validate widgets
        var errors = new List<Error>();
        
        foreach (var widget in widgets)
        {
            foreach (var error in widget.Validate())
            {
                errors.Add(error);
            }
        }
        
        foreach (var error in errors)
        {
            ErrorManager.Add(error.Message);
        }

        return widgets;
    }

    private void ApplyDefaults(WidgetConfig config, Dictionary<string, string> defaults)
    {
        foreach (var property in config.GetType().GetProperties().Where(p => p.CanWrite))
        {
            if (property.GetValue(config) != null)
            {
                continue;
            }

            if (!defaults.TryGetValue(property.Name.ToLower(), out var defaultValue))
            {
                continue;
            }

            var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            var converted = Convert.ChangeType(defaultValue, targetType);
            property.SetValue(config, converted);
        }
    }
}