using System.Runtime.InteropServices;

namespace ScreenStats.App.Native;

public static class NativeMethods
{
    public const int GWL_EXSTYLE = -20;

    public const int WS_EX_TOOLWINDOW = 0x80;
    public const int WS_EX_NOACTIVATE = 0x08000000;
    public const int WS_EX_TRANSPARENT = 0x20;
    public const int WS_EX_LAYERED = 0x80000;

    public const int WM_SPAWN_WORKER = 0x052C;

    public const uint SWP_NOMOVE = 0x0002;
    public const uint SWP_NOSIZE = 0x0001;
    public const uint SWP_NOACTIVATE = 0x0010;

    public static readonly uint SWP_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE;

    public static readonly IntPtr HWND_BOTTOM = new(1);

    public enum SendMessageTimeoutFlags
    {
        SMTO_NORMAL = 0x0
    }

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern IntPtr FindWindowEx(
        IntPtr parent,
        IntPtr childAfter,
        string className,
        string windowName
    );

    [DllImport("user32.dll")]
    public static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessageTimeout(
        IntPtr hWnd,
        int msg,
        IntPtr wParam,
        IntPtr lParam,
        SendMessageTimeoutFlags flags,
        int timeout,
        out IntPtr result
    );

    [DllImport("user32.dll")]
    public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    public static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x,
        int y,
        int cx,
        int cy,
        uint flags
    );
}