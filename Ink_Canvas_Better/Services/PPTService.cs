using System.Diagnostics;
using System.Runtime.InteropServices;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Office.Interop.PowerPoint;
using PPTApp = Microsoft.Office.Interop.PowerPoint.Application;

namespace Ink_Canvas_Better.Services;

public class PPTService
{
    private readonly System.Timers.Timer PPTCheckTimer = new()
    {
        AutoReset = true,
        Interval = 200
    };

    private readonly ILogger logger = IApp.GetService<ILogger<PPTService>>();
    private PPTApp? PPTApplication;

    public PPTService()
    {
        PPTCheckTimer.Elapsed += PPTCheckTimer_Elapsed;
        PPTCheckTimer.Start();
    }

    private void PPTCheckTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            PPTApplication = DllHelper.GetActiveObject("PowerPoint.Application") as PPTApp;
            if (PPTApplication != null)
            {
                ConnectPPT();
                logger.LogInformation("PPT connected");
            }
        }
        catch (COMException ex)
        {
            logger.LogWarning("Occurs in PPTService.PPTCheckTimer_Elapsed()" + ex.ToString());
        }
    }

    private void ConnectPPT()
    {
        try
        {
            PPTCheckTimer.Stop();
            if (PPTApplication is null) throw new NullReferenceException();
            PPTApplication.PresentationOpen += PPTApplication_PresentationOpen;
            PPTApplication.PresentationCloseFinal += PPTApplication_PresentationCloseFinal;
            PPTApplication.SlideShowBegin += PPTApplication_SlideShowBegin;
        }
        catch (Exception ex)
        {
            logger.LogWarning("Occurs in PPTService.ConnectPPT() " + ex.ToString());
        }
    }

    private void DisconnectPPT()
    {
        try
        {
            PPTCheckTimer.Start();
            if ( PPTApplication is not null)
            {
                PPTApplication.PresentationOpen -= PPTApplication_PresentationOpen;
                PPTApplication.PresentationCloseFinal -= PPTApplication_PresentationCloseFinal;
                PPTApplication.SlideShowBegin -= PPTApplication_SlideShowBegin;
            }
            PPTApplication = null;
        }
        catch (Exception ex)
        {
            logger.LogWarning("Occurs in PPTService.DisconnectPPT() " + ex.ToString());
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
        logger.LogInformation($"SlideShow Begin, path:{PPTApplication.ActivePresentation.FullName}");
    }
}
