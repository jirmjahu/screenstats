using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Errors;

namespace ScreenStats.App.Config;

public static class ConfigLoader
{
    public static AppConfig Load(string path)
    {
        ErrorManager.Clear();

        if (!File.Exists(path))
        {
            CreateDefaultConfig(path);
        }

        var config = new ConfigurationBuilder()
            .AddIniFile(path, optional: false)
            .Build();

        var appConfig = new AppConfig();

        config.GetSection("background").Bind(appConfig.Background);
        config.GetSection("layout").Bind(appConfig.Layout);

        foreach (var section in config.GetSection("defaults").GetChildren())
        {
            appConfig.Defaults[section.Key.ToLower()] = section.Value ?? string.Empty;
        }
        
        foreach (var section in config.GetSection("widgets").GetChildren())
        {
            if (!int.TryParse(section.Key, out var index))
            {
                ErrorManager.Add("Invalid widget index! Widget indexes must be a non decimal number");
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
            else if (type == "drive")
            {
                widget = section.Get<DriveUsageConfig>();
            }
            else if (type == "weather")
            {
                widget = section.Get<WeatherWidgetConfig>();
            }
            else
            {
                ErrorManager.Add($"Unknown widget type: {type}");
                continue;
            }

            if (widget != null)
            {
                appConfig.Widgets[index] = widget;
            }
        }

        return appConfig;
    }

    private static void CreateDefaultConfig(string path)
    {
        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var defaultConfigContent = LoadDefaultConfig();
        File.WriteAllText(path, defaultConfigContent);
    }

    private static string LoadDefaultConfig()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "ScreenStats.App.Assets.config.ini";

        using var stream = assembly.GetManifestResourceStream(resourceName) ??
                           throw new FileNotFoundException("Default config not found in assembly.");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}