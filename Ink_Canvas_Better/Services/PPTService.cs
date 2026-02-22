using System.Diagnostics;
using System.Runtime.InteropServices;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
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
    private PPTApp? PPTApplication;

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
            PPTApplication = DllHelper.GetActiveObject("PowerPoint.Application") as PPTApp;
            
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
            if (PPTApplication is null || PPTApplication.Presentations.Count == 0)
            {
                PPTCheckTimer.Start();
            }
            else
            {
                PPTApplication.PresentationOpen += PPTApplication_PresentationOpen;
                PPTApplication.PresentationCloseFinal += PPTApplication_PresentationCloseFinal;
                PPTApplication.SlideShowBegin += PPTApplication_SlideShowBegin;
                logger.WriteLog(LogLevel.Information, $"PPT connected");
                Debug.WriteLine(PPTApplication.Presentations.Count);
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
            if (PPTApplication is not null)
            {
                PPTApplication.PresentationOpen -= PPTApplication_PresentationOpen;
                PPTApplication.PresentationCloseFinal -= PPTApplication_PresentationCloseFinal;
                PPTApplication.SlideShowBegin -= PPTApplication_SlideShowBegin;
                Marshal.FinalReleaseComObject(PPTApplication);
                PPTApplication = null;
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
        logger.WriteLog(LogLevel.Information, () => $"SlideShow Begin, path:{PPTApplication.ActivePresentation.FullName}");
        Debug.WriteLine(PPTApplication.SlideShowWindows.Count);
        foreach (SlideShowWindow item in PPTApplication.SlideShowWindows)
        {
            Debug.WriteLine(item.Width);
            Debug.WriteLine(item.Height);
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
