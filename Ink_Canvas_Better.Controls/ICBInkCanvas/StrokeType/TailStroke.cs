using System;
using System.Windows.Ink;
using System.Windows.Input;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

class TailStroke : Stroke
{
    /// <summary>
    /// It determines the number of stylusPoint where the stroke effect needs to be applied.
    /// </summary>
    public int EffectLength { get; set; } = 10;

    /// <summary>
    /// Create stroke with special tail.
    /// </summary>
    /// <remarks>
    /// Related StylusPlugin: (none)<br/>
    /// </remarks>
    public TailStroke(StylusPointCollection rawStylusPoints, DrawingAttributes drawingAttributes)
        : base(rawStylusPoints, drawingAttributes)
    {

        this.DrawingAttributes = drawingAttributes;

        var newStylusPoints = new StylusPointCollection();
        var count = rawStylusPoints.Count - 1;
        var pressure = 0.1;
        if (count == 0) return;
        if (count >= EffectLength)
        {
            for (var i = 0; i < count - EffectLength; i++)
            {
                var point = new StylusPoint(
                    rawStylusPoints[i].X,
                    rawStylusPoints[i].Y,
                    rawStylusPoints[i].PressureFactor);
                newStylusPoints.Add(point);
            }

            for (var i = count - EffectLength; i <= count; i++)
            {
                var point = new StylusPoint(
                    rawStylusPoints[i].X,
                    rawStylusPoints[i].Y,
                    (float)((0.5 - pressure) * (count - i) / EffectLength + pressure));
                newStylusPoints.Add(point);
            }
        }
        else
        {
            for (var i = 0; i <= count; i++)
            {
                var point = new StylusPoint(
                    rawStylusPoints[i].X,
                    rawStylusPoints[i].Y,
                    (float)(0.4 * (count - i) / count + pressure));
                newStylusPoints.Add(point);
            }
        }

        StylusPoints = newStylusPoints;
    }
}
