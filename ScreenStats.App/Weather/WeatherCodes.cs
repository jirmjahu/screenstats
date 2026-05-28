namespace ScreenStats.App.Weather;

public static class WeatherCodes
{
    // Source: https://github.com/AlienDwarf/open-meteo-dotnet/blob/master/OpenMeteo/OpenMeteoClient.cs
    private static readonly Dictionary<int, WeatherEntry> Entries = new()
    {
        [0] = new("Clear Sky", "☀", "☾"),
        [1] = new("Mainly Clear", "☀", "☾"),
        [2] = new("Partly Cloudy", "⛅", "☁"),
        [3] = new("Overcast", "☁"),
        [45] = new("Fog", "🌫"),
        [48] = new("Depositing rime Fog", "🌫"),
        [51] = new("Light drizzle", "🌦"),
        [53] = new("Moderate drizzle", "🌦"),
        [55] = new("Dense drizzle", "🌦"),
        [56] = new("Light freezing drizzle", "🌦"),
        [57] = new("Dense freezing drizzle", "🌦"),
        [61] = new("Slight rain", "🌧"),
        [63] = new("Moderate rain", "🌧"),
        [65] = new("Heavy rain", "🌧"),
        [66] = new("Light freezing rain", "🌧"),
        [67] = new("Heavy freezing rain", "🌧"),
        [71] = new("Slight snow fall", "❄"),
        [73] = new("Moderate snow fall", "❄"),
        [75] = new("Heavy snow fall", "❄"),
        [77] = new("Snow grain", "❄"),
        [80] = new("Slight rain showers", "🌧"),
        [81] = new("Moderate rain showers", "🌧"),
        [82] = new("Violent rain showers", "🌧"),
        [85] = new("Slight snow showers", "❄"),
        [86] = new("Heavy snow showers", "❄"),
        [95] = new("Thunderstorm", "⛈"),
        [96] = new("Thunderstorm with light Hail", "⛈"),
        [99] = new("Thunderstorm with heavy Hail", "⛈"),
    };

    public static string Description(int code) =>
        Entries.TryGetValue(code, out var entry) ? entry.Description : "Unknown";

    public static string Icon(int code, bool isDay) =>
        Entries.TryGetValue(code, out var entry) ? entry.GetIcon(isDay) : "·";

    private record WeatherEntry(string Description, string DayIcon, string? NightIcon = null)
    {
        public string GetIcon(bool isDay) => !isDay && NightIcon != null ? NightIcon : DayIcon;
    }
}