using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Ink_Canvas_Better.Services;

public class MultiscreenService : IDisposable
{
    private readonly ILogger logger;

    public MultiscreenService(ILogger<MultiscreenService> logger)
    {
        this.logger = logger;
        DllHelper.CheckScreens();
        ApplyToMainWindow();
        SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
    }

    private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs e)
    {
        logger.LogInformation("Display settings changed");
        DllHelper.CheckScreens();
        ApplyToMainWindow();
    }

    private void ApplyToMainWindow()
    {
        int width = DllHelper.Screens[0].Width;
        int height = DllHelper.Screens[0].Height;
        if (DllHelper.Screens.Count > 1)
        {
            foreach (var item in DllHelper.Screens)
            {
                width = Math.Max(width, item.X + item.Width);
                height = Math.Max(height, item.Y + item.Height);
            }
        }
        IApp.GetService<MainWindow>().Width = width;
        IApp.GetService<MainWindow>().Height = height;
        logger.LogInformation($"MainWindow has resized to {width}x{height}");
    }

    public void Dispose()
    {
        SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
    }
}
