using System.Windows;
using System.Windows.Interop;
using ScreenStats.App.Native;

namespace ScreenStats.App.Helpers;

public static class WindowHelper
{
    /// <summary>
    /// Makes the window behave like a desktop widget
    /// </summary>
    /// <param name="window">The window</param>
    public static void MakeDesktopWidget(Window window)
    {
        var hwnd = new WindowInteropHelper(window).Handle;
        var progman = NativeMethods.FindWindow("Progman", null);

        // Move this window behind every other
        NativeMethods.SetWindowPos(
            hwnd,
            NativeMethods.HWND_BOTTOM,
            0,
            0,
            0,
            0,
            NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE
        );

        // Set Progman as owner of the window so it is bound to the desktop
        // This also prevents it from being hidden by Win+D or the Show Desktop button
        NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWLP_HWNDPARENT, progman);

        // Apply widget styles
        var style = NativeMethods.GetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE);

        style |= NativeMethods.WS_EX_TOOLWINDOW
                 | NativeMethods.WS_EX_NOACTIVATE
                 | NativeMethods.WS_EX_TRANSPARENT
                 | NativeMethods.WS_EX_LAYERED;

        NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE, style);
    }
}