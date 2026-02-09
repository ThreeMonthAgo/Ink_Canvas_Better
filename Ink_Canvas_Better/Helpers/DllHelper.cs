using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Ink_Canvas_Better.Helpers
{
    internal partial class DllHelper
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

        public static readonly ObservableCollection<RECT> Screens = [];
        private static readonly unsafe MONITORENUMPROC proc = new(MonitorEnumProc); // Keep this alive

        /// <summary>
        /// Gets information about all display monitors connected to the system.
        /// </summary>
        /// <returns>A list of <see cref="Screen"/> objects representing each monitor.</returns>
        public static unsafe void CheckScreens()
        {
            Screens.Clear();
            BOOL fRes = PInvoke.EnumDisplayMonitors(HDC.Null, null, MonitorEnumProc, 0);
        }

        private static unsafe BOOL MonitorEnumProc(HMONITOR hMonitor, HDC hdcMonitor, RECT* lprcMonitor, LPARAM dwData)
        {
            MONITORINFOEXW info = default;
            info.monitorInfo.cbSize = (uint)sizeof(MONITORINFO);
            PInvoke.GetMonitorInfo(hMonitor, (MONITORINFO*)&info);
            Screens.Add(*lprcMonitor);
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
}
