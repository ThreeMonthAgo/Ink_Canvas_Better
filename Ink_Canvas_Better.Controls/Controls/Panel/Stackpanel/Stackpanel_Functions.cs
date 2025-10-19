using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class Stackpanel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            double totalHeight = 0, totalWidth = 0;
            switch (Orientation)
            {
                case Orientation.Horizontal:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        child.Measure(new Size(double.PositiveInfinity, availableSize.Height));
                        totalWidth += child.DesiredSize.Width;
                        totalHeight = Math.Max(totalHeight, child.DesiredSize.Height);
                    }
                    if (InternalChildren.Count > 1)
                        totalWidth += Spacing * (InternalChildren.Count - 1);
                    break;
                case Orientation.Vertical:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        child.Measure(new Size(availableSize.Width, double.PositiveInfinity));
                        totalHeight += child.DesiredSize.Height;
                        totalWidth = Math.Max(totalWidth, child.DesiredSize.Width);
                    }
                    if (InternalChildren.Count > 1)
                        totalHeight += Spacing * (InternalChildren.Count - 1);
                    break;
            }
            return new Size(totalWidth, totalHeight);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double currentPosition = 0;

            switch (Orientation)
            {
                case Orientation.Horizontal:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        double childWidth = child.DesiredSize.Width;
                        double childHeight = child.DesiredSize.Height;
                        Rect rect = new Rect(currentPosition, 0, childWidth, childHeight);
                        child.Arrange(rect);
                        currentPosition += childWidth + Spacing;
                    }
                    break;

                case Orientation.Vertical:
                    foreach (UIElement child in InternalChildren)
                    {
                        if (child == null) continue;
                        double childWidth = child.DesiredSize.Width;
                        double childHeight = child.DesiredSize.Height;
                        Rect rect = new Rect(0, currentPosition, childWidth, childHeight);
                        child.Arrange(rect);
                        currentPosition += childHeight + Spacing;
                    }
                    break;
            }
            return finalSize;
        }
    }
}
