using System.Diagnostics;
using Ink_Canvas_Better.Helpers;
using PPTApp = Microsoft.Office.Interop.PowerPoint.Application;

namespace Ink_Canvas_Better.Services;

public class PPTService
{
    private readonly System.Timers.Timer PPTCheckTimer = new()
    {
        AutoReset = true,
        Interval = 100
    };

    public PPTService()
    {
        PPTCheckTimer.Elapsed += PPTCheckTimer_Elapsed;
        PPTCheckTimer.Start();
    }

    private void PPTCheckTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Debug.WriteLine("Hitted 1");
        var pptApplication = DllHelper.GetActiveObject("PowerPoint.Application") as PPTApp;
        Debug.WriteLine("Hitted 2");
    }
}
