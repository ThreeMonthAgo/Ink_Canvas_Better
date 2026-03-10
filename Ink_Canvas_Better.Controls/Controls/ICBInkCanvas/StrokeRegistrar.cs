using System.Collections.ObjectModel;
using System.Windows.Ink;
using System.Windows.Input.StylusPlugIns;
using Ink_Canvas_Better.Controls.Controls.ICBInkCanvas.StylusPlugIns;
using Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

namespace Ink_Canvas_Better.Controls.Controls.ICBInkCanvas;

public class StrokeRegistrar
{
    public Collection<StrokeInfo> RegisteredStrokes =
        [
            new(typeof(Stroke), typeof(DynamicRenderer)), // Default stroke
            new(typeof(SpeedStroke), typeof(SpeedStrokeStylusPlugIn))
        ];

    public void Register(Type strokeType, Type stylusPlugInType) =>
        RegisteredStrokes.Add(new(strokeType, stylusPlugInType));
}

public class StrokeInfo(Type strokeType, Type stylusPlugInType)
{
    public Type StrokeType { get; } = strokeType;
    public Type StylusPlugInType { get; } = stylusPlugInType;
}
