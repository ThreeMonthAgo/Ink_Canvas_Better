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
    private readonly MainWindow mainWindow;

    public MultiscreenService(ILogger<MultiscreenService> logger, MainWindow mainWindow)
    {
        this.logger = logger;
        this.mainWindow = mainWindow;

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
        mainWindow.Width = width;
        mainWindow.Height = height;
        logger.WriteLog(LogLevel.Information, () => $"MainWindow has resized to {width}x{height}");
        // Floating bar
        var tbCollection = IApp.Settings.MainWindowVM.ToolBarCollection;
        while (screenCount > tbCollection.Count) tbCollection.Add(new());
        // TODO: while (screenCount < fbCollection.Count) fbCollection.RemoveLast();
        for (int i = 0; i < screenCount; i++)
        {
            var tb = tbCollection[i];
            tb.ScreenIndex = i;
            tb.Dock();
        }
        logger.WriteLog(LogLevel.Trace, () => $"Floating bar amount: {tbCollection.Count}");
        if (logger.IsEnabled(LogLevel.Trace))
        {
            for (int i = 0; i < tbCollection.Count; i++)
            {
                logger.WriteLog(LogLevel.Trace, () => $"Floating bar {i}: x:{tbCollection[i].X} y:{tbCollection[i].Y}");
            }
        }
    }

    public void Dispose()
    {
        SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
        GC.SuppressFinalize(this);
    }
}
