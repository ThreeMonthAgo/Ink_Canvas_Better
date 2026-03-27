using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Helpers.Converter;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Utilities.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.PowerPoint;
using PPTApp = Microsoft.Office.Interop.PowerPoint.Application;

namespace Ink_Canvas_Better.Services;

public class PPTService(ILogger<PPTService> logger)
{
    private readonly System.Timers.Timer PPTCheckTimer = new()
    {
        AutoReset = false,
        Interval = 1000
    };

    private readonly ILogger logger = logger;
    private dynamic? PPTApplication;

    public void Init()
    {
        PPTCheckTimer.Elapsed += PPTCheckTimer_Elapsed;
        PPTCheckTimer.Start();
    }

    private void PPTCheckTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            PPTCheckTimer.Enabled = false;
            PPTApplication = DllHelper.GetActiveObject("PowerPoint.Application");
            
            ConnectPPT();
        }
        catch (COMException ex)
        {
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.PPTCheckTimer_Elapsed() {ex}");
            PPTCheckTimer.Start();
        }
    }

    private void ConnectPPT()
    {
        try
        {
            var ppt = PPTApplication as PPTApp;
            if (ppt is null || PPTApplication.Presentations.Count == 0)
            {
                PPTCheckTimer.Start();
            }
            else
            {
                ppt.PresentationOpen += PPTApplication_PresentationOpen;
                ppt.PresentationCloseFinal += PPTApplication_PresentationCloseFinal;
                ppt.SlideShowBegin += PPTApplication_SlideShowBegin;
                ppt.SlideShowEnd += PPTApplication_SlideShowEnd;
                logger.WriteLog(LogLevel.Information, $"PPT connected");
            }
        }
        catch (Exception ex)
        {
            PPTCheckTimer.Start();
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.ConnectPPT() {ex}");
        }
    }

    private void DisconnectPPT()
    {
        try
        {
            if (PPTApplication is PPTApp ppt)
            {
                ppt.PresentationOpen -= PPTApplication_PresentationOpen;
                ppt.PresentationCloseFinal -= PPTApplication_PresentationCloseFinal;
                ppt.SlideShowBegin -= PPTApplication_SlideShowBegin;
                ppt.SlideShowEnd -= PPTApplication_SlideShowEnd;
                Marshal.FinalReleaseComObject(PPTApplication);
                ppt = null;
            }
            logger.WriteLog(LogLevel.Information, "PPT disconnected");
        }
        catch (Exception ex)
        {
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.DisconnectPPT() {ex}");
        }
        finally
        {
            PPTCheckTimer.Start();
        }
    }

    private void PPTApplication_PresentationOpen(Presentation p)
    {

    }

    private void PPTApplication_PresentationCloseFinal(Presentation p)
    {
        DisconnectPPT();
    }

    private void PPTApplication_SlideShowBegin(SlideShowWindow s)
    {
        var p = ((PPTApp)PPTApplication).ActivePresentation;
        logger.WriteLog(LogLevel.Information, () => $"SlideShow Begin, path:{p.FullName}");
        var c = IApp.GetService<SettingsService>().Settings.MainWindowVM.SlideShowControlCollection;
        if (((PPTApp)PPTApplication).WindowState == PpWindowState.ppWindowNormal)
        {
            foreach (var item in c)
            {
                // TODO: Use screenIndex instead of ppt window data
                item.Dock(p.SlideShowWindow.ToRect() * (p.SlideShowWindow.View.Zoom / 100d));
                Debug.WriteLine(p.SlideShowWindow.Width);
                Debug.WriteLine(p.SlideShowWindow.Height);
                Debug.WriteLine(p.SlideShowWindow.View.Zoom);
                Debug.WriteLine(item.X);
                Debug.WriteLine(item.Y);
                item.Visibility = Visibility.Visible;
            }
        }
        else
        {
            // Dock
        }
    }

    private void PPTApplication_SlideShowEnd(Presentation Pres)
    {
        var c = IApp.GetService<SettingsService>().Settings.MainWindowVM.SlideShowControlCollection;
        foreach (var item in c)
        {
            item.Visibility = Visibility.Collapsed;
        }
    }

    public void Previous()
    {
        try
        {
            PPTApplication.ActivePresentation.SlideShowWindow.View.Previous();
        }
        catch (Exception ex)
        {
            logger.WriteLog(LogLevel.Error, ex.ToString);
        }
    }

    public void Next()
    {
        try
        {
            PPTApplication.ActivePresentation.SlideShowWindow.View.Next();
        }
        catch (Exception ex)
        {
            logger.WriteLog(LogLevel.Error, ex.ToString);
        }
    }
}
