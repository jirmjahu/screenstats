using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ScreenStats.App.Info.Models;
using ScreenStats.App.Weather.Models;

namespace ScreenStats.App.Weather;

public static class WeatherClient
{
    private const string GeocodingUrl = "https://geocoding-api.open-meteo.com/v1/search";
    private const string ForecastUrl = "https://api.open-meteo.com/v1/forecast";
    private const string CurrentFields = "temperature_2m,relative_humidity_2m,apparent_temperature,is_day,weather_code,wind_speed_10m";

    private static readonly HttpClient HttpClient = new();
    private static readonly JsonSerializerOptions JsonOptions = new();

    public static async Task<GeocodingResult?> GeocodeAsync(string country, string city)
    {
        var url = $"{GeocodingUrl}?name={Uri.EscapeDataString(city)}&count=10&language=en&format=json";
        using var response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<GeocodingResponse>(JsonOptions);

        if (data?.Results == null || data.Results.Length == 0)
        {
            return null;
        }

        return PickBestMatch(data.Results, country);
    }

    public static async Task<WeatherInfoData?> GetCurrentWeatherAsync(double latitude, double longitude, string temperatureUnit, string windSpeedUnit)
    {
        var url =
            $"{ForecastUrl}?latitude={latitude.ToString(CultureInfo.InvariantCulture)}&longitude={longitude.ToString(CultureInfo.InvariantCulture)}&current={CurrentFields}" +
            $"&temperature_unit={temperatureUnit}&wind_speed_unit={windSpeedUnit}";

        using var response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<WeatherResponse>(JsonOptions);

        if (data?.Current == null)
        {
            return null;
        }

        return new WeatherInfoData
        {
            Temperature = data.Current.Temperature,
            ApparentTemperature = data.Current.ApparentTemperature,
            Humidity = data.Current.Humidity,
            WindSpeed = data.Current.WindSpeed,
            WeatherCode = data.Current.WeatherCode,
            IsDay = data.Current.IsDay == 1,
            TemperatureUnit = data.CurrentUnits?.Temperature,
            WindSpeedUnit = data.CurrentUnits?.WindSpeed
        };
    }

    private static GeocodingResult PickBestMatch(GeocodingResult[] results, string country)
    {
        foreach (var result in results)
        {
            if (string.Equals(result.Country, country, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(result.CountryCode, country, StringComparison.OrdinalIgnoreCase))
            {
                return result;
            }
        }

        return results[0];
    }
}