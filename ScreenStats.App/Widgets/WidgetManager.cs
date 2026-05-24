using ScreenStats.App.Config.Models;
using ScreenStats.App.Widgets.Types;

namespace ScreenStats.App.Widgets;

public class WidgetManager
{
    public List<Widget> Widgets { get; set; } = [];

    public void Load(AppConfig config)
    {
        Widgets.Clear();
        Widgets.AddRange(CreateWidgetsFromConfig(config));
    }

    /// <summary>
    /// Creates widget objects from the app configuration
    /// </summary>
    /// <param name="appConfig">The configuration with widget settings</param>
    /// <returns>The list of created widgets</returns>
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
            }
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

    public void UpdateWidgets()
    {
        foreach (var widget in Widgets)
        {
            if (widget is UpdateableWidget updateable)
            {
                updateable.Update();
            }
        }
    }
}