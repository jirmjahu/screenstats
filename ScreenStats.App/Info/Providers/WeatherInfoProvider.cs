using ScreenStats.App.Info.Models;
using ScreenStats.App.Weather;

namespace ScreenStats.App.Info.Providers;

public static class WeatherInfoProvider
{
    public static async Task<WeatherInfoData?> GetAsync(string city, string country, string temperatureUnit, string windSpeedUnit)
    {
        var location = await WeatherClient.GeocodeAsync(country, city);

        if (location == null)
        {
            return null;
        }

        var data = await WeatherClient.GetCurrentWeatherAsync(
            location.Latitude,
            location.Longitude,
            temperatureUnit,
            windSpeedUnit);

        if (data == null)
        {
            return null;
        }

        data.LocationName = location.Name;

        return data;
    }
}