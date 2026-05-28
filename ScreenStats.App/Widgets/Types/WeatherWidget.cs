using System.Windows.Controls;
using ScreenStats.App.Config.Models;
using ScreenStats.App.Controls;
using ScreenStats.App.Info;

namespace ScreenStats.App.Widgets.Types;

public class WeatherWidget(WeatherWidgetConfig config) : UpdateableWidget
{
    public WeatherWidgetConfig Config { get; } = config;

    public string? DisplayText
    {
        get;
        set
        {
            if (field == value)
            {
                return;
            }

            field = value;
            OnPropertyChanged();
        }
    } = "";
    
    protected override async Task Update()
    {
        if (string.IsNullOrWhiteSpace(Config.City) || string.IsNullOrWhiteSpace(Config.Country))
        {
            DisplayText = "Missing city and country in config";
            return;
        }

        var weather = await SystemInfo.GetWeather(Config.City, Config.Country, Config.TemperatureUnit, Config.WindSpeedUnit);
        if (weather == null)
        {
            DisplayText = "Loading weather...";
            return;
        }

        DisplayText = Config.Content!
            .Replace("\\n", Environment.NewLine)
            .Replace("{icon}", weather.Icon)
            .Replace("{description}", weather.Description)
            .Replace("{temp}", weather.Temperature.ToString("0.0"))
            .Replace("{temp_unit}", weather.TemperatureUnit)
            .Replace("{feels_like}", weather.ApparentTemperature.ToString("0.0"))
            .Replace("{humidity}", weather.Humidity.ToString("0"))
            .Replace("{wind}", weather.WindSpeed.ToString("0.0"))
            .Replace("{wind_unit}", weather.WindSpeedUnit)
            .Replace("{location}", weather.LocationName);
    }
    
    public override UserControl GetControl()
    {
        return new WeatherWidgetControl(this);
    }
    
    protected override TimeSpan UpdateInterval()
    {
        return TimeSpan.FromMinutes(10);
    }
    
}