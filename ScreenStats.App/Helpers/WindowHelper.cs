using System.Windows;
using System.Windows.Interop;
using ScreenStats.App.Native;

namespace ScreenStats.App.Helpers;

public static class WindowHelper
{ 
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
        // Using Program also prevents the window from being hidden by Win+D
        NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWLP_HWNDPARENT, progman);

        // Apply styles
        var style = NativeMethods.GetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE);

        style |= NativeMethods.WS_EX_TOOLWINDOW
                 | NativeMethods.WS_EX_NOACTIVATE
                 | NativeMethods.WS_EX_TRANSPARENT
                 | NativeMethods.WS_EX_LAYERED;

        NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWL_EXSTYLE, style);
    }
}