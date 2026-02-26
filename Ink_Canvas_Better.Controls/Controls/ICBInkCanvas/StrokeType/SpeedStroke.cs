using System.Windows.Ink;
using System.Windows.Input;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

class SpeedStroke : Stroke
{
    /// <summary>
    /// Stroke with thickness that changes according to speed
    /// </summary>
    /// <remarks>
    /// Related StylusPlugin: (TODO: DynamicRenderder)<br/>
    /// </remarks>
    public SpeedStroke(StylusPointCollection rawStylusPoints, DrawingAttributes drawingAttributes)
        : base(rawStylusPoints, drawingAttributes)
    {
        this.DrawingAttributes = drawingAttributes.Clone();
        var newStylusPoints = new StylusPointCollection();
        for (int i = 0; i < rawStylusPoints.Count; i++)
        {
            if (i == 0)
            {
                newStylusPoints.Add(rawStylusPoints[i]);
                continue;
            };
            // TODO: Better appearance and algorithm
            double dx = Math.Abs(rawStylusPoints[i].X - rawStylusPoints[i - 1].X);
            double dy = Math.Abs(rawStylusPoints[i].Y - rawStylusPoints[i - 1].Y);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            float pressure = (float)(1d / (distance + 10) * 5);
            newStylusPoints.Add(new StylusPoint()
            {
                X = rawStylusPoints[i].X,
                Y = rawStylusPoints[i].Y,
                PressureFactor = pressure
            });
        }
        StylusPoints = newStylusPoints;
    }
}
