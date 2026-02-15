using System.Runtime.InteropServices;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Utilities.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.PowerPoint;
using PPTApp = Microsoft.Office.Interop.PowerPoint.Application;

namespace Ink_Canvas_Better.Services;

public class PPTService
{
    private readonly System.Timers.Timer PPTCheckTimer = new()
    {
        AutoReset = false,
        Interval = 500
    };

    private readonly ILogger logger = IApp.GetService<ILogger<PPTService>>();
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
            if (PPTApplication is not null) ConnectPPT();
            else PPTCheckTimer.Start();
        }
        catch (COMException ex)
        {
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.PPTCheckTimer_Elapsed() {ex}");
            PPTCheckTimer.Start();
        }
    }

    private bool ConnectPPT()
    {
        try
        {
            if (PPTApplication is null)
            {
                return false;
            }
            else
            {
                PPTApplication.PresentationOpen += PPTApplication_PresentationOpen;
                PPTApplication.PresentationCloseFinal += PPTApplication_PresentationCloseFinal;
                PPTApplication.SlideShowBegin += PPTApplication_SlideShowBegin;
                logger.WriteLog(LogLevel.Information, $"PPT connected");
                return true;
            }
        }
        catch (Exception ex)
        {
            PPTCheckTimer.Start();
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.ConnectPPT() {ex}");
            return false;
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
                PPTApplication = null;
            }
            PPTCheckTimer.Start();
            logger.WriteLog(LogLevel.Information, "PPT disconnected");
        }
        catch (Exception ex)
        {
            PPTCheckTimer.Start();
            logger.WriteLog(LogLevel.Warning, () => $"Occurs in PPTService.DisconnectPPT() {ex}");
        }
    }

    private void PPTApplication_PresentationOpen(Presentation Pres)
    {

    }

    private void PPTApplication_PresentationCloseFinal(Presentation Pres)
    {
        DisconnectPPT();

    }

    private void PPTApplication_SlideShowBegin(SlideShowWindow Wn)
    {
        logger.WriteLog(LogLevel.Information, () => $"SlideShow Begin, path:{PPTApplication.ActivePresentation.FullName}");
    }
}
