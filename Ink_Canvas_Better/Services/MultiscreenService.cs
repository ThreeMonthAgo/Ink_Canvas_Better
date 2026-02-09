using System.ComponentModel;
using System.Runtime.CompilerServices;
using Ink_Canvas_Better.Helpers;
using Microsoft.Win32;

namespace Ink_Canvas_Better.Services;

public class MultiscreenService : IDisposable
{
    public MultiscreenService()
    {
        DllHelper.CheckScreens();
        SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
    }

    private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs e)
    {
        DllHelper.CheckScreens();
    }

    public void Dispose()
    {
        SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
    }
}
