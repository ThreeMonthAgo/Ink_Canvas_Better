using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Helpers
{
    public class VisualTreeHelper
    {
        public static T? GetVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject? parentObject = System.Windows.Media.VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject.GetType() == typeof(T))
            {
                return (T)parentObject;
            }
            else
            {
                return GetVisualParent<T>(parentObject);
            }
        }

        public static object GetVisualParent(DependencyObject child, Type targetType)
        {
            DependencyObject? parentObject = System.Windows.Media.VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject.GetType() == targetType)
            {
                return parentObject;
            }
            else
            {
                return GetVisualParent(parentObject, targetType);
            }
        }

        public static T? GetLogicalParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject? parentObject = System.Windows.LogicalTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject.GetType() == typeof(T))
            {
                return (T)parentObject;
            }
            else
            {
                return GetLogicalParent<T>(parentObject);
            }
        }

        public static object GetLogicalParent(DependencyObject child, Type targetType)
        {
            DependencyObject? parentObject = System.Windows.LogicalTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject.GetType() == targetType)
            {
                return parentObject;
            }
            else
            {
                return GetLogicalParent(parentObject, targetType);
            }
        }
    }
}
