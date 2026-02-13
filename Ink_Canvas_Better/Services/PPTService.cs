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
        Interval = 500
    };

    private readonly ILogger logger = IApp.GetService<ILogger<PPTService>>();
    private PPTApp? PPTApplication;

    public PPTService()
    {
        PPTCheckTimer.Elapsed += PPTCheckTimer_Elapsed;
    }

    public void RunCheckTimer(bool b)
    {
        if (b) PPTCheckTimer.Start();
        else PPTCheckTimer.Stop();
    }

    private void PPTCheckTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        try
        {
            PPTApplication = DllHelper.GetActiveObject("PowerPoint.Application") as PPTApp;
            if (PPTApplication != null)
            {
                if (ConnectPPT())
                {
                    logger.LogInformation("PPT connected");
                }
            }
        }
        catch (COMException ex)
        {
            logger.LogWarning("Occurs in PPTService.PPTCheckTimer_Elapsed()" + ex.ToString());
        }
    }

    private bool ConnectPPT()
    {
        try
        {
            if (PPTApplication is null) throw new NullReferenceException();
            PPTApplication.PresentationOpen += PPTApplication_PresentationOpen;
            PPTApplication.PresentationCloseFinal += PPTApplication_PresentationCloseFinal;
            PPTApplication.SlideShowBegin += PPTApplication_SlideShowBegin;
            RunCheckTimer(false);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogWarning("Occurs in PPTService.ConnectPPT() " + ex.ToString());
            return false;
        }
    }

    private void DisconnectPPT()
    {
        try
        {
            RunCheckTimer(true);
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
