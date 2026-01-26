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
    /// Stroke with special tail.
    /// </summary>
    /// <remarks>
    /// Related StylusPlugin: (none)<br/>
    /// </remarks>
    public TailStroke(StylusPointCollection rawStylusPoints, DrawingAttributes drawingAttributes)
        : base(rawStylusPoints, drawingAttributes)
    {
        this.DrawingAttributes = drawingAttributes.Clone();
        var newStylusPoints = new StylusPointCollection();
        var count = rawStylusPoints.Count - 1;
        if (count == 0) return;
        if (count >= EffectLength)
        {
            for (var i = 0; i < count - EffectLength; i++)
            {
                var pressure = rawStylusPoints[i].PressureFactor;
                Add(rawStylusPoints[i], pressure, newStylusPoints);
            }

            for (var i = count - EffectLength; i <= count; i++)
            {
                var pressure = (float)(0.5 * (count - i) / EffectLength);
                Add(rawStylusPoints[i], pressure, newStylusPoints);
            }
        }
        else
        {
            for (var i = 0; i <= count; i++)
            {
                var pressure = (float)(0.5 * (count - i) / count);
                Add(rawStylusPoints[i], pressure, newStylusPoints);
            }
        }
        StylusPoints = newStylusPoints;

        // func
        void Add(StylusPoint rawPoint, float pressureFactor, StylusPointCollection stylusPoints)
        {
            var point = new StylusPoint(rawPoint.X, rawPoint.Y, pressureFactor);
            newStylusPoints.Add(point);
        }
    }
}
