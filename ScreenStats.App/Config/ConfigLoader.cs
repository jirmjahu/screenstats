using Microsoft.Extensions.Configuration;
using ScreenStats.App.Config.Models;

namespace ScreenStats.App.Config;

public static class ConfigLoader
{
    public static AppConfig Load(string basePath, string path)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddIniFile(path, optional: false)
            .Build();

        var appConfig = new AppConfig();

        config.GetSection("background").Bind(appConfig.Background);
        config.GetSection("layout").Bind(appConfig.Layout);

        foreach (var section in config.GetSection("widgets").GetChildren())
        {
            if (!int.TryParse(section.Key, out var index))
            {
                continue;
            }

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
            else if (type == "media")
            {
                widget = section.Get<MediaWidgetConfig>();           
            }

            if (widget != null)
            {
                appConfig.Widgets[index] = widget;
            }
        }

        return appConfig;
    }
}