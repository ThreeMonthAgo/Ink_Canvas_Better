using System.Runtime.InteropServices;
using System.Security;
using Ink_Canvas_Better.Utilities.DataStructures;
using iNKORE.UI.WPF.Modern.Native;

namespace Ink_Canvas_Better.Helpers
{
    internal class DllHelper
    {
        #region Windows

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_TRANSPARENT = 0x00000020;

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public static int AddExtendedStyle(nint handle, int style)
        {
            int extendedStyle = GetWindowLong(handle, GWL_EXSTYLE);
            return SetWindowLong(
                handle,
                GWL_EXSTYLE,
                extendedStyle | style
            );
        }

        public static int RemoveExtendedStyle(nint handle, int style)
        {
            int extendedStyle = GetWindowLong(handle, GWL_EXSTYLE);
            return SetWindowLong(
                handle,
                GWL_EXSTYLE,
                extendedStyle &~ style
            );
        }

        public static int SetExtendedStyle(nint handle, int style)
        {
            return SetWindowLong(
                handle,
                GWL_EXSTYLE,
                style
            );
        }

        #endregion

        #region Monitors

        private static List<Screen> _screens;

        /// <summary>
        /// Gets information about all display monitors connected to the system.
        /// </summary>
        /// <returns>A list of <see cref="Screen"/> objects representing each monitor.</returns>
        public static List<Screen> GetScreens()
        {
            _screens = [];

            DisplayDevicesMethods.EnumDisplayMonitors(
                IntPtr.Zero,
                IntPtr.Zero,
                MonitorEnumProc,
                IntPtr.Zero);

            return _screens;
        }

        private static bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, RECT rect, IntPtr dwData)
        {
            DisplayDevicesMethods.MonitorInfo mi = new();
            if (DisplayDevicesMethods.GetMonitorInfo(hMonitor, mi))
            {
                _screens.Add(new Screen(
                    (mi.dwFlags & 1) == 1, // Flag 1 indicates primary monitor
                    mi.rcMonitor.left,
                    mi.rcMonitor.top,
                    mi.rcMonitor.Width,
                    mi.rcMonitor.Height));
            }
            return true;
        }

        internal static class DisplayDevicesMethods
        {
            internal delegate bool EnumMonitorsDelegate(
                IntPtr hMonitor,
                IntPtr hdcMonitor,
                RECT rect,
                IntPtr dwData);

            /// <summary>
            /// Enumerates display monitors in the system.
            /// </summary>
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool EnumDisplayMonitors(
                IntPtr hdc,
                IntPtr lprcClip,
                EnumMonitorsDelegate lpfnEnum,
                IntPtr dwData);

            /// <summary>
            /// Retrieves information about a display monitor.
            /// </summary>
            /// <returns><see langword="true"/> if successful; otherwise, <see langword="false"/>.</returns>
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool GetMonitorInfo(
                IntPtr hmonitor,
                [In, Out] MonitorInfo info);

            /// <summary>
            /// Contains information about a display monitor.
            /// </summary>
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
            internal class MonitorInfo
            {
                internal int cbSize = Marshal.SizeOf<MonitorInfo>();
                internal RECT rcMonitor = new();
                internal RECT rcWork = new();
                internal int dwFlags;
            }
        }

        #endregion

        #region ole

        [DllImport("ole32.dll")]
        private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        [DllImport("ole32.dll")]
        private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        [DllImport("oleaut32.dll")]
        private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

        public static object GetActiveObject(string progID)
        {
            Guid clsid;
            try
            {
                CLSIDFromProgIDEx(progID, out clsid);
            }
            catch
            {
                CLSIDFromProgID(progID, out clsid);
            }
            GetActiveObject(ref clsid, IntPtr.Zero, out var obj);
            return obj;
        }

        #endregion
    }
}
