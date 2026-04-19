using Microsoft.Extensions.Configuration;
using ScreenStats.App.Config.Models;

namespace ScreenStats.App.Config;

public static class ConfigLoader
{
    public static AppConfig Load(string basePath, string path)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddIniFile(path, optional: false, reloadOnChange: true)
            .Build();

        var appConfig = new AppConfig
        {
            Background = new BackgroundConfig(),
            Widgets = new Dictionary<string, WidgetConfig>()
        };

        config.GetSection("background").Bind(appConfig.Background);

        foreach (var section in config.GetSection("widgets").GetChildren())
        {
            var type = section.GetValue<string>("Type");

            WidgetConfig? widget = null;

            if (type == "text")
            {
                widget = section.Get<TextWidgetConfig>();
            }
            else if (type == "cpu")
            {
                widget = section.Get<CpuWidgetConfig>();
            }
            else if (type == "ram")
            {
                widget = section.Get<RamWidgetConfig>();
            }

            if (widget != null)
            {
                appConfig.Widgets[section.Key] = widget;
            }
        }

        return appConfig;
    }
}