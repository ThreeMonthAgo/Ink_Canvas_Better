using System.Windows.Input;
using System.Windows.Input.StylusPlugIns;

namespace Ink_Canvas_Better.Controls.Controls.ICBInkCanvas.StylusPlugIns;

public class SpeedStrokeStylusPlugIn : StylusPlugIn
{
    StylusPoint? prevPoint = null;
    StylusPointCollection collectedPoints;

    protected override void OnStylusDown(RawStylusInput rawStylusInput)
    {
        base.OnStylusDown(rawStylusInput);

        var rawPoints = rawStylusInput.GetStylusPoints();
        collectedPoints = new StylusPointCollection(rawPoints.Description);
        StylusPointCollection points = Modify(rawPoints);
        rawStylusInput.SetStylusPoints(points);
        collectedPoints.Add(points);
    }

    protected override void OnStylusMove(RawStylusInput rawStylusInput)
    {
        base.OnStylusMove(rawStylusInput);

        var rawPoints = rawStylusInput.GetStylusPoints();
        StylusPointCollection points = Modify(rawPoints);
        rawStylusInput.SetStylusPoints(points);
        collectedPoints.Add(points);
    }

    protected override void OnStylusUp(RawStylusInput rawStylusInput)
    {
        base.OnStylusUp(rawStylusInput);

        var rawPoints = rawStylusInput.GetStylusPoints();
        StylusPointCollection points = Modify(rawPoints);
        rawStylusInput.SetStylusPoints(points);
        collectedPoints.Add(points);

        rawStylusInput.NotifyWhenProcessed(collectedPoints);
    }

    private StylusPointCollection Modify(StylusPointCollection points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (prevPoint != null)
            {
                float pressure = GetPressure((StylusPoint)prevPoint, points[i]);
                StylusPoint pt = points[i];
                pt.PressureFactor = pressure;
                points[i] = pt;
            }
            prevPoint = points[i];
        }
        return points;
    }

    private static float GetPressure(StylusPoint p1, StylusPoint p2)
    {
        double dx = Math.Abs(p1.X - p2.X);
        double dy = Math.Abs(p1.Y - p2.Y);
        double distance = Math.Sqrt(dx * dx + dy * dy);
        // TODO: Customize arguments
        return (float)(1d / (distance + 10) * 5);
    }
}
