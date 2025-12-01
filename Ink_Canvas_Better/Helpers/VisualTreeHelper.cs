using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Ink_Canvas_Better.Helpers
{
    public class VisualTreeHelper
    {
        public static T? GetParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject? parentObject = System.Windows.Media.VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;
            if (parentObject.GetType() == typeof(T))
            {
                return (T)parentObject;
            }
            else
            {
                return GetParent<T>(parentObject);
            }
        }
    }
}
