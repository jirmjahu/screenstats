using System.Collections.Concurrent;
using ScreenStats.App.Info.Models;
using ScreenStats.App.Weather;

namespace ScreenStats.App.Info.Providers;

public static class WeatherInfoProvider
{
    private static readonly TimeSpan RefreshInterval = TimeSpan.FromMinutes(10);
    private static readonly ConcurrentDictionary<string, CacheEntry> Cache = new();

    public static WeatherInfoData? Get(string city, string country, string temperatureUnit, string windSpeedUnit)
    {
        var key = $"{city.Trim().ToLower()}{country.Trim().ToLower()}";
        Cache.TryGetValue(key, out var entry);

        if (entry == null || DateTime.UtcNow - entry.LastAttempt >= RefreshInterval)
        {
            Cache[key] = new CacheEntry(entry?.Data, DateTime.UtcNow);
            _ = RefreshAsync(key, city, country, temperatureUnit, windSpeedUnit);
        }

        return entry?.Data;
    }

    private static async Task RefreshAsync(string key, string city, string country, string temperatureUnit, string windSpeedUnit)
    {
        var location = await WeatherClient.GeocodeAsync(country, city);
        if (location == null)
        {
            return;
        }

        var data = await WeatherClient.GetCurrentWeatherAsync(location.Latitude, location.Longitude, temperatureUnit, windSpeedUnit);
        if (data == null)
        {
            return;
        }

        data.LocationName = location.Name;
        Cache[key] = new CacheEntry(data, DateTime.UtcNow);
    }

    private record CacheEntry(WeatherInfoData? Data, DateTime LastAttempt);
}
