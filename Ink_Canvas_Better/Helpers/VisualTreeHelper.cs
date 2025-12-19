using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

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

        public static DependencyObject GetVisualParent(DependencyObject child, Collection<Type> types)
        {
            DependencyObject? parentObject = System.Windows.Media.VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            return types.Contains(parentObject.GetType()) ? parentObject : GetVisualParent(parentObject, types);
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

    }
}
