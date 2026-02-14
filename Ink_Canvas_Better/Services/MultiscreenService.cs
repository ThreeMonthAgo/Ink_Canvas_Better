using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Ink_Canvas_Better.Services;

public class MultiscreenService : IDisposable
{
    private readonly ILogger logger;
    private readonly SettingsService settingsService;

    public MultiscreenService(ILogger<MultiscreenService> logger, SettingsService settingsService)
    {
        this.logger = logger;
        this.settingsService = settingsService;

        SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
    }

    public void Check()
    {
        logger.WriteLog(LogLevel.Information, "Display settings changed");
        DllHelper.CheckScreens();
        ApplyToMainWindow();
    }

    private void SystemEvents_DisplaySettingsChanged(object? sender, EventArgs e) => Check();

    private void ApplyToMainWindow()
    {
        int width = DllHelper.Screens[0].Width;
        int height = DllHelper.Screens[0].Height;
        int screenCount = DllHelper.Screens.Count;
        logger.WriteLog(LogLevel.Trace, () => $"Screen acount: {screenCount}");
        if (screenCount > 1)
        {
            foreach (var item in DllHelper.Screens)
            {
                width = Math.Max(width, item.X + item.Width);
                height = Math.Max(height, item.Y + item.Height);
            }
        }
        // MainWindow
        IApp.GetService<MainWindow>().Width = width;
        IApp.GetService<MainWindow>().Height = height;
        logger.WriteLog(LogLevel.Information, () => $"MainWindow has resized to {width}x{height}");
        // Floating bar
        var fbCollection = settingsService.Settings.MainWindowVM.FloatingBarCollection;
        while (screenCount > fbCollection.Count) fbCollection.Add(new());
        // TODO: while (screenCount < fbCollection.Count) fbCollection.RemoveLast();
        for (int i = 0; i < screenCount; i++)
        {
            var fb = fbCollection[i];
            fb.ScreenIndex = i;
            fb.Dock();
        }
        logger.LogTrace($"Floating bar amount: {fbCollection.Count}");
        if (logger.IsEnabled(LogLevel.Trace))
        {
            for (int i = 0; i < fbCollection.Count; i++)
            {
                logger.LogTrace($"Floating bar {i}: x:{fbCollection[i].X} y:{fbCollection[i].Y}");
            }
        }
    }

    public void Dispose()
    {
        SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
        GC.SuppressFinalize(this);
    }
}
