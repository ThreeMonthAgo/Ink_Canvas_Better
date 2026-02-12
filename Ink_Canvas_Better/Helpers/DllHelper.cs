using System.Collections.ObjectModel;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Ink_Canvas_Better.Helpers;

public partial class DllHelper
{
    #region Windows

    public const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int WS_EX_TRANSPARENT = 0x00000020;

    public static int AddExtendedStyle(HWND hwnd, int style)
    {
        int extendedStyle = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        return PInvoke.SetWindowLong(
            hwnd,
            WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE,
            extendedStyle | style
        );
    }

    public static int RemoveExtendedStyle(HWND hwnd, int style)
    {
        int extendedStyle = PInvoke.GetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
        return PInvoke.SetWindowLong(
            hwnd,
            WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE,
            extendedStyle &~ style
        );
    }

    public static int SetExtendedStyle(HWND hwnd, int style)
    {
        return PInvoke.SetWindowLong(
            hwnd,
            WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE,
            style
        );
    }

    #endregion

    #region Monitors

    public struct MonitorInfo
    {
        internal string name;
        internal uint flags;
        internal int x;
        internal int y;
        internal int width;
        internal int height;
        internal int wkaWidth;
        internal int wkaHeight;

        public readonly string Name => name;
        public readonly uint Flags => flags;
        public readonly int X => x;
        public readonly int Y => y;
        public readonly int Width => width;
        public readonly int Height => height;
        public readonly int WkaWidth => wkaWidth;
        public readonly int WkaHeight => wkaHeight;
    }

    public static readonly ObservableCollection<MonitorInfo> Screens = [];
    private static readonly unsafe MONITORENUMPROC proc = new(MonitorEnumProc);

    /// <summary>
    /// Gets information about all display monitors connected to the system.
    /// </summary>
    public static unsafe void CheckScreens()
    {
        Screens.Clear();
        BOOL fRes = PInvoke.EnumDisplayMonitors(HDC.Null, null, proc, 0);
    }

    private static unsafe BOOL MonitorEnumProc(HMONITOR hMonitor, HDC hdcMonitor, RECT* lprcMonitor, LPARAM dwData)
    {
        MONITORINFOEXW info = default;
        info.monitorInfo.cbSize = (uint)sizeof(MONITORINFOEXW);
        PInvoke.GetMonitorInfo(hMonitor, (MONITORINFO*)&info);
        Screens.Add(new()
        {
            name = info.szDevice.ToString(),
            flags = info.monitorInfo.dwFlags,
            x = info.monitorInfo.rcMonitor.X,
            y = info.monitorInfo.rcMonitor.Y,
            width = info.monitorInfo.rcMonitor.Width,
            height = info.monitorInfo.rcMonitor.Height,
            wkaWidth = info.monitorInfo.rcWork.Width,
            wkaHeight = info.monitorInfo.rcWork.Height
        });
        return true;
    }

    #endregion

    #region ole

    public static unsafe object GetActiveObject(string progID)
    {
        Guid clsid;
        try
        {
            PInvoke.CLSIDFromProgIDEx(progID, out clsid);
        }
        catch
        {
            PInvoke.CLSIDFromProgID(progID, out clsid);
        }
        PInvoke.GetActiveObject(in clsid, null, out object obj);
        return obj;
    }

    #endregion
}
