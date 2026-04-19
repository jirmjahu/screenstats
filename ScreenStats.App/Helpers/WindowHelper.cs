using System;
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
        var worker = GetWorkerW();

        // Attach the window to the WorkerW window (that will make it stay behind the desktop icons)
        if (worker != IntPtr.Zero)
        {
            NativeMethods.SetParent(hwnd, worker);
        }

        // Move this window behind every other
        NativeMethods.SetWindowPos(
            hwnd,
            NativeMethods.HWND_BOTTOM,
            0,
            0,
            0,
            0,
            NativeMethods.SWP_FLAGS
        );

        // Apply widget styles
        var style = NativeMethods.GetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE);

        style |= NativeMethods.WS_EX_TOOLWINDOW
                 | NativeMethods.WS_EX_NOACTIVATE
                 | NativeMethods.WS_EX_TRANSPARENT
                 | NativeMethods.WS_EX_LAYERED;

        NativeMethods.SetWindowLong(hwnd, NativeMethods.GWL_EXSTYLE, style);
    }

    private static IntPtr GetWorkerW()
    {
        var progman = NativeMethods.FindWindow("Progman", null);

        NativeMethods.SendMessageTimeout(
            progman,
            NativeMethods.WM_SPAWN_WORKER,
            IntPtr.Zero,
            IntPtr.Zero,
            NativeMethods.SendMessageTimeoutFlags.SMTO_NORMAL,
            1000,
            out _
        );

        var current = IntPtr.Zero;

        while ((current = NativeMethods.FindWindowEx(IntPtr.Zero, current, "WorkerW", null)) != IntPtr.Zero)
        {
            if (NativeMethods.FindWindowEx(current, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
            {
                return current;
            }
        }

        return IntPtr.Zero;
    }
}