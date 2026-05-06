using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using ScreenStats.App.Info;

namespace ScreenStats.App.Text;

public static class PlaceholderReplacer
{
    public static string? Replace(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return null;
        }
        
        var ram = SystemInfo.GetRam();

        return text
            .Replace("{username}", Environment.UserName)
            .Replace("{computer}", Environment.MachineName)
            .Replace("{os}", GetOsName())
            .Replace("{arch}", RuntimeInformation.OSArchitecture.ToString())
            .Replace("{date}", DateTime.Now.ToString("dd.MM.yyyy"))
            .Replace("{time}", DateTime.Now.ToString("HH:mm:ss"))
            .Replace("{day}", DateTime.Now.ToString("dddd"))
            .Replace("{uptime}", FormatUptime(TimeSpan.FromMilliseconds(Environment.TickCount64)))
            .Replace("{ip}", GetLocalIpAddress())
            .Replace("{cpu_cores}", Environment.ProcessorCount.ToString())
            .Replace("{ram_used}", ram.UsedGb.ToString("0.0"))
            .Replace("{ram_total}", ram.TotalGb.ToString("0.0"))
            .Replace("{ram_available}", ram.AvailableGb.ToString("0.0"));
    }

    private static string GetOsName()
    {
        var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        return key?.GetValue("ProductName") as string ?? Environment.OSVersion.VersionString;
    }

    private static string GetLocalIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        return string.Empty;
    }

    private static string FormatUptime(TimeSpan uptime)
    {
        return $"{uptime.Days}d {uptime.Hours}h {uptime.Minutes}m {uptime.Seconds}s";
    }
}