using System.Windows;

namespace Ink_Canvas_Better.Controls.Helpers;

/// <summary>
/// Provides functions to find ascendant.
/// </summary>
/// <remarks>
/// For VisualTree, please use <see cref="iNKORE.UI.WPF.Helpers.VisualTree"/>
/// </remarks>
public static class LogicalTreeHelper
{
    public static T? FindAscendant<T>(DependencyObject element) where T : DependencyObject
    {
        var parent = System.Windows.LogicalTreeHelper.GetParent(element);
        if (parent == null)  return null;
        if (parent.GetType() == typeof(T)) return (T)parent;
        return FindAscendant<T>(parent);
    }

    public static object FindAscendant(this DependencyObject element, Type targetType)
    {
        var parent = System.Windows.LogicalTreeHelper.GetParent(element);
        if (parent == null) return null;
        if (parent.GetType() == targetType) return parent;
        return parent.FindAscendant(targetType);
    }
}
