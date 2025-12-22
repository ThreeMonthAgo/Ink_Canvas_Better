namespace Ink_Canvas_Better.Utilities.DataStructures;

/// <summary>
/// Used for multi-screen display support
/// </summary>
internal sealed class Screen(bool isPrimary, int x, int y, int width, int height)
{
    /// <summary>
    /// Gets a value indicating whether this screen is the primary display.
    /// </summary>
    internal bool IsPrimary { get; private set; } = isPrimary;

    internal int X { get; private set; } = x;

    internal int Y { get; private set; } = y;

    internal int Width { get; private set; } = width;

    internal int Height { get; private set; } = height;
}
