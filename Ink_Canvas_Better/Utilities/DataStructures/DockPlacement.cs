using static Ink_Canvas_Better.Utilities.Enums.UI;

namespace Ink_Canvas_Better.Utilities.DataStructures;

/// <summary>
/// See <see cref="Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBar.Dock(DockPlacement)"/>
/// </summary>
public class DockPlacement
{
    public DockVerticalAlignment VerticalAlignment { get; set; } = DockVerticalAlignment.Center;

    public DockHorizontalAlignment HorizontalAlignment { get; set; } = DockHorizontalAlignment.Center;
}
